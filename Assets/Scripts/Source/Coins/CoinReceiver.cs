using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinReceiver : MonoBehaviour, ITracker
{
    private int _count;
    private Saving _data;

    void Start()
    {
        if(!TryGetComponent<Saving>(out _data))
        {
            Debug.LogWarning("Saving is null.");
        }
        
        _count = _data.PlayerData.Coins;
    }

    public void CollectCoin(int count)
    {
        _count += count;
        _data.PlayerData.Coins = _count;
    }

    public int Count()
    {
        return _count;
    }
}
