using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public static AudioManager Instance;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    public void Play(string name)
    {
        IEnumerable<Sound> results = from x in Sounds
            where x.Name == name
            select x;

        Sound sound = results.FirstOrDefault();

        if(sound == null)
        {
            Debug.LogWarning("Cannont find sound with name: " + name);
            return;
        }

        sound.Source.Play();
    }
}