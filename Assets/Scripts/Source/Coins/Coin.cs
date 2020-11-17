using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(BoxCollider))]
public class Coin : MonoBehaviour, IPoolable
{
    public int Cost = 1;

    private Renderer _renderer;
    private BoxCollider _collider;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<BoxCollider>();
        DisableCoin();
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if(!_renderer.enabled)
            {
                return;
            }
            
            _renderer.enabled = false;
            collider.gameObject.SendMessage("CollectCoin", Cost);
        }
    }

    public void OnSpawn()
    {
        EnableCoin();
    }

    private void DisableCoin()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    private void EnableCoin()
    {
        _renderer.enabled = true;
        _collider.enabled = true;
    }
}