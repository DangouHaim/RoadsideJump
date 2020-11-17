using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinReceiver : MonoBehaviour, ITracker
{
    private int _count;
    private DataService _service;

    void Start()
    {
        if(!GameObject.FindGameObjectWithTag("DataService").TryGetComponent<DataService>(out _service))
        {
            Debug.LogWarning("DataService is null.");
        }
        
        _count = _service.UserModel.Coins;
    }

    public void CollectCoin(int count)
    {
        _count += count;
        _service.UserModel.Coins = _count;
        AudioManager.Instance.Play("Coin");
    }

    public int Count()
    {
        return _count;
    }
}