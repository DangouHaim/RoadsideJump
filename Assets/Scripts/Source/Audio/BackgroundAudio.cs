﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    public float Interval = 10;
    public float VolumeIncreaseDuraion = 0.05f;
    public string[] Sounds;

    private static BackgroundAudio _instance;
    private static bool _firstStepDone = false;

    void Awake()
    {
        DontDestroyOnLoad(this);

        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!_firstStepDone)
        {
            _firstStepDone = transform;
            StartCoroutine(StartPlaing());
        }
    }

    private IEnumerator StartPlaing()
    {
        while(true)
        {
            foreach(string s in Sounds)
            {
                AudioSource source = AudioManager.Instance.Play(s);
                
                if(s != Sounds[0])
                {
                    yield return IncreaseVolume(source);
                }

                yield return new WaitWhile( () => { return source.isPlaying; } );
                yield return new WaitForSeconds(Interval);
            }
        }
    }

    private IEnumerator IncreaseVolume(AudioSource source)
    {
        float volumePercent = source.volume / 100f;
        source.volume = 0;
        
        for(int i = 0; i < 100; i++)
        {
            source.volume += volumePercent;
            yield return new WaitForSeconds(VolumeIncreaseDuraion);
        }
    }
}
