﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : Generator
{
    void Start()
    {
        OnRegularGenerated += (s, e) =>
        {
            pool.Spawn("Tree", e.SpawnVector, Quaternion.identity);
        };

        OnSpecificGenerated += (s, e) =>
        {
            pool.Spawn("Coin", e.SpawnVector, Quaternion.identity);
        };
        base.OnStart();
    }
}
