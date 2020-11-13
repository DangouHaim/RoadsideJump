using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathTracker : MonoBehaviour, ITracker
{
    public event EventHandler<EventArgs> RecordAchived = (s, e) => {};

    private Saving _data;

    void Start()
    {
        if(!TryGetComponent<Saving>(out _data))
        {
            Debug.LogWarning("Saving is null.");
        }
    }

    void FixedUpdate()
    {
        int count = Count();

        if(count > _data.PlayerData.Path)
        {
            RecordAchived.Invoke(this, new EventArgs());
            _data.PlayerData.Path = count;
        }
    }

    public int Count()
    {
        return -(int)transform.position.z;
    }
}
