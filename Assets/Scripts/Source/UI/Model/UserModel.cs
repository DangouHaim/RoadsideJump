using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}