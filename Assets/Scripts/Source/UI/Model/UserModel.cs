using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserModel : BindableBase
{
    private int _path;
    public int Path
    {
        get
        {
            return _path;
        }
        set
        {
            if(value == _path)
            {
                return;
            }
            _path = value;
            OnPropertyChanged("Path", _path);
        }
    }

    private int _pathRecord;
    public int PathRecord
    {
        get
        {
            return _pathRecord;
        }
        set
        {
            if(value == _pathRecord)
            {
                return;
            }
            _pathRecord = value;
            OnPropertyChanged("PathRecord", _pathRecord);
            Save(this);
        }
    }

    private int _coins;
    public int Coins
    {
        get
        {
            return _coins;
        }
        set
        {
            if(value == _coins)
            {
                return;
            }
            _coins = value;
            OnPropertyChanged("Coins", _coins);
            Save(this);
        }
    }

    private bool _isDead;
    public bool IsDead
    {
        get
        {
            return _isDead;
        }
        set
        {
            if(value == _isDead)
            {
                return;
            }
            _isDead = value;
            OnPropertyChanged("IsDead", _isDead);
        }
    }

    private bool _isStarted;
    public bool IsStarted
    {
        get
        {
            return _isStarted;
        }
        set
        {
            if(value == _isStarted)
            {
                return;
            }
            _isStarted = value;
            OnPropertyChanged("IsStarted", _isStarted);
        }
    }

    public UserModel()
    {

    }

    public UserModel(UserModel source)
    {
        Path = source.Path;
        PathRecord = source.PathRecord;
        Coins = source.Coins;
        IsDead = source.IsDead;
        IsStarted = source.IsStarted;
    }
}