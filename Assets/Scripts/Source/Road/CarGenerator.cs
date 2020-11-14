using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventArgs = System.EventArgs;

public class CarGenerator : MonoBehaviour
{
    public class GenerationEventArgs : EventArgs
    {
        public int Seconds {get;set;}
    }

    public  event System.EventHandler<GenerationEventArgs> BeforeGeneration = (s, e) => {};

    public string CarPrefabName = "Car";
    public int MinimumCarDuration = 8;
    public int MaximumCarDuration = 15;
    public int MinimumSpawnTime = 2;
    public int MaximumSpawnTime = 5;
    public bool LateMove = false;

    private PoolManager _pool;
    private int _direction;

    void Start()
    {
        _pool = PoolManager.Instance;
        _direction = Random.Range(0, 2);

        StartCoroutine("Generate");
    }

    private IEnumerator Generate()
    {
        while(true)
        {
            int wait = Random.Range(MinimumSpawnTime, MaximumSpawnTime);
            float duration = Random.Range(MinimumCarDuration, MaximumCarDuration);
            
            float allowed = GetAllowedDuration(wait);
            
            if(duration < allowed) // Detect minimum allowed motion duration to prevent collision
            {
                duration = allowed;
            }

            Vector3 start = transform.Find("Start").transform.position;
            Vector3 end = transform.Find("End").transform.position;
            
            Vector3 carStart = start;
            Vector3 carEnd = end;

            // Detect direction
            bool toLeft = _direction == 1;
            if(toLeft)
            {
                // To left
                carStart = end;
                carEnd = start;
            }

            IMovable car = _pool.Spawn(CarPrefabName, carStart, Quaternion.identity).GetComponent<IMovable>();
            
            if(!LateMove)
            {
                // Move and rotate car
                car.MoveTo(carEnd, duration, toLeft);
            }

            BeforeGeneration.Invoke(this, new GenerationEventArgs() { Seconds = wait});
            yield return new WaitForSeconds(wait);

            if(LateMove)
            {
                // Move and rotate car
                car.MoveTo(carEnd, duration, toLeft);
            }
            LateMove = true;
        }
    }

    private float GetAllowedDuration(int wait)
    {
        float max = MaximumCarDuration;
        float min = MinimumCarDuration;

        // Get absolute ranges
        float total = max - min;
        int totalWait = MaximumSpawnTime - MinimumSpawnTime;

        // Get increasing percent
        float percent = total / totalWait;

        float result = max - percent * (wait - MinimumSpawnTime);

        return result;
    }
}