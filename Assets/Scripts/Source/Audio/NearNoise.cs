using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearNoise : MonoBehaviour, IPoolable
{
    public float NoiseDistance = 5;
    public string NoiseSound;

    private bool _played = false;
    private GameObject _player;

    void Start()
    {
        _played = false;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void FixedUpdate()
    {
        MakeNoise();
    }

    public void OnSpawn()
    {
        _played = false;
    }

    private void MakeNoise()
    {
        AudioSource noise = AudioManager.Instance.Find(NoiseSound);

        if(Distance() <= NoiseDistance)
        {
            if(_played)
            {
                return;
            }
            _played = true;
            
            noise.PlayOneShot(noise.clip);
        }
    }

    private float Distance()
    {
        Vector3 playerPosition = _player.transform.position;
        float distance = Vector3.Distance(transform.position, playerPosition);
        return distance;
    }
}