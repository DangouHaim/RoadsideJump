using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : Generator
{
    void Start()
    {
        OnSpecificGenerated += (s, e) =>
        {
            pool.Spawn("Coin", e.SpawnVector, Quaternion.identity);
        };
        base.OnStart();
    }
}
