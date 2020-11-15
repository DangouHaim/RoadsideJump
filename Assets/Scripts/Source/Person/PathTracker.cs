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
        if(!GameObject.FindGameObjectWithTag("Saving").TryGetComponent<Saving>(out _data))
        {
            Debug.LogWarning("Saving is null.");
        }
    }

    void FixedUpdate()
    {
        int count = Count();

        if(count > _data.UserModel.Path)
        {
            _data.UserModel.Path = count;
        }

        if(count > _data.PlayerData.PathRecord)
        {
            RecordAchived.Invoke(this, new EventArgs());
            _data.PlayerData.PathRecord = count;
            _data.UserModel.PathRecord = count;
        }
    }

    public int Count()
    {
        return -(int)transform.position.z;
    }
}
