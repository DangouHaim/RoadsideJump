using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathTracker : MonoBehaviour, ITracker
{
    public event EventHandler<EventArgs> RecordAchived = (s, e) => {};

    private DataService  _service;

    void Start()
    {
        if(!GameObject.FindGameObjectWithTag("DataService").TryGetComponent<DataService>(out _service))
        {
            Debug.LogWarning("DataService is null.");
        }

        // Reset path on respawn
        _service.UserModel.Path = 0;
    }

    void FixedUpdate()
    {
        int count = Count();

        if(count > _service.UserModel.Path)
        {
            _service.UserModel.Path = count;
        }

        if(count > _service.UserModel.PathRecord)
        {
            RecordAchived.Invoke(this, new EventArgs());
            _service.UserModel.PathRecord = count;
        }
    }

    public int Count()
    {
        return -(int)transform.position.z;
    }
}
