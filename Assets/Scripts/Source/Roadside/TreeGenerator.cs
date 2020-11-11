using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour, IPoolable
{
    public int Width = 36;

    private PoolManager _pool;
    private bool _isInit = false;

    // Start is called before the first frame update
    void Start()
    {
        _pool = PoolManager.Instance;

        transform.localScale = new Vector3(
            (float)Width,
            transform.localScale.y,
            transform.localScale.z
        );

        GenerateTrees();
        _isInit = true;
    }

    public void OnSpawn()
    {
        if(_isInit)
        {
            GenerateTrees();
        }
    }

    private void GenerateTrees()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        int part = Width / 3;
        
        // Center in zero => left side = Width / 2; right side = left side * -1
        // If we have Width = 6 and center = 0 => roadsice have next coordinates for generation:
        // 3, 2, 1, 0, -1, -2, -3 
        int start = Width / 2;

        for(int i = start; i > -start; i--)
        {
            int factor = 10; // Maximum random value

            if(i < -part / 2 || i > part / 2)
            {
                factor = 3; // High generation chanse near roadside left or right corner
            }

            int result = Random.Range(1, factor);

            if(result == 1)
            {
                Vector3 spawnVector = new Vector3((float)-i, transform.position.y + 1, transform.position.z);

                if(Vector3.Distance(player.transform.position, spawnVector) > 1) // Check if player far enought
                {
                    _pool.Spawn("Tree", spawnVector, Quaternion.identity);
                }
            }

        }
    }
}
