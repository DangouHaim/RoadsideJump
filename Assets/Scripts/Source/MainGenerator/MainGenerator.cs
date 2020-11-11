using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGenerator : MonoBehaviour
{
    public int StartRoadsideLenght = 10;

    private PoolManager _pool;

    // Start is called before the first frame update
    void Start()
    {
        _pool = PoolManager.Instance;

        GenerateStartRoadside();
    }

    private void GenerateStartRoadside()
    {
        for(int i = 0; i < StartRoadsideLenght; i++)
        {
            Vector3 spawnVector = new Vector3(
                transform.position.x,
                transform.position.y,
                (float)i
            );

            _pool.Spawn("Roadside", spawnVector, Quaternion.identity);
        }
    }
}
