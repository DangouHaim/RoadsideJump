using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;

    [Range(0, 1)]
    public float Volume;
    [Range(0.1f, 3)]
    public float Pitch;
    public bool Loop;

    [HideInInspector]
    public AudioSource Source;
}
