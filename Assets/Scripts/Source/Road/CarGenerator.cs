using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    public int MinimumCarDuration = 8;
    public int MaximumCarDuration = 15;
    public int MinimumSpawnTime = 2;
    public int MaximumSpawnTime = 5;

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

            IMovable car = _pool.Spawn("Car", carStart, Quaternion.identity).GetComponent<IMovable>();
            // Move and rotate car
            car.MoveTo(carEnd, Random.Range(MinimumCarDuration, MaximumCarDuration), toLeft);

            int wait = Random.Range(MinimumSpawnTime, MaximumSpawnTime);
            yield return new WaitForSeconds(wait);
        }
    }
}