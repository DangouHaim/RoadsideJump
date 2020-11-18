using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearNoise : MonoBehaviour, IPoolable
{
    public float NoiseDistance = 5;
    public string NoiseSound;

    private bool _played = false;
    private GameObject _player;

    public void OnSpawn()
    {
        _played = false;
    }

    void Start()
    {
        _played = false;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MakeNoise();
    }

    private void MakeNoise()
    {
        Vector3 playerPosition = _player.transform.position;

        if(Vector3.Distance(transform.position, playerPosition) <= NoiseDistance)
        {
            if(_played)
            {
                return;
            }
            _played = true;
            
            AudioManager.Instance.Play(NoiseSound);
        }
    }
}