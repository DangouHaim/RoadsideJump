using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizeShadows : MonoBehaviour
{
    private int _frames = 0;
    private int _badFrames = 0;
    private bool _shadpwsDisabled = false;

    void Update()
    {
        if(_shadpwsDisabled || _frames > 15)
        {
            return;
        }

        _frames++;

        if(1.0f / Time.smoothDeltaTime < 40)
        {
            _badFrames++;
        }

        if(_badFrames > 10)
        {
            QualitySettings.shadows = ShadowQuality.Disable;
            _shadpwsDisabled = true;
        }
    }
}
