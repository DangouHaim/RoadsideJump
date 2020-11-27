using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public abstract class Generator : MonoBehaviour, IPoolable
{
    public class GenerationEventArgs : EventArgs
    {
        public Vector3 SpawnVector {get; set;}
    }

    public event EventHandler<GenerationEventArgs> OnRegularGenerated = (s, e) => {};
    public event EventHandler<GenerationEventArgs> OnSpecificGenerated = (s, e) => {};

    public Vector3 SpawnOffset = new Vector3(0, 1, 0);
    
    protected PoolManager pool;
    
    private bool _isInit = false;
    private int _width;

    // Start is called before the first frame update
    protected void OnStart()
    {
        pool = PoolManager.Instance;
        _width = (int)transform.localScale.x;

        Generate();
        _isInit = true;
    }

    public void OnSpawn()
    {
        if(_isInit)
        {
            Generate();
        }
    }

    private void Generate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        int generatedInMiddle = 0;

        int part = _width / 3;
        
        // Center in zero => left side = Width / 2; right side = left side * -1
        // If we have Width = 6 and center = 0 => roadsice have next coordinates for generation:
        // 3, 2, 1, 0, -1, -2, -3 
        int start = _width / 2;

        for(int i = start; i > -start; i--)
        {
            int factor = 10; // Maximum random value
            bool isCenter = true;

            if(i < -part / 2 || i > part / 2)
            {
                factor = 3; // High generation chanse near left or right corner
                isCenter = false;
            }

            int result = Random.Range(1, factor);

            if(result == 1)
            {
                if(factor == 10) // If middle part
                {
                    generatedInMiddle++;
                }

                if(generatedInMiddle > part / 2) // If too many objects in middle part
                {
                    continue;
                }

                Vector3 spawnVector = new Vector3(
                    (float)-i + SpawnOffset.x,
                    transform.position.y + SpawnOffset.y,
                    transform.position.z + SpawnOffset.z
                );

                if(Vector3.Distance(player.transform.position, spawnVector) > 2) // Check if player far enought
                {
                    // 1 to 10 generation factor in center
                    if(isCenter && Random.Range(0, 8) == 4)
                    {
                        OnSpecificGenerated.Invoke(
                            this,
                            new GenerationEventArgs()
                            {
                                SpawnVector = spawnVector
                            }
                        );
                    }
                    else
                    {
                        OnRegularGenerated.Invoke(
                            this,
                            new GenerationEventArgs()
                            {
                                SpawnVector = spawnVector
                            }
                        );
                    }
                }
            }

        }
    }
}