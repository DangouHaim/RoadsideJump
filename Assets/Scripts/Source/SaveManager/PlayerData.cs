using System;
using UnityEngine;

[Serializable]
public class PlayerData : AutoSaveBase
{   
    private int _coins;
    public int Coins
    {
        get { return _coins; }
        set
        {
            if(_coins == value)
            {
                return;
            }
            
            _coins = value;

            Save(this);
        }
    }

    private int _pathRecord;
    public int PathRecord
    {
        get { return _pathRecord; }
        set
        {
            if(_pathRecord == value)
            {
                return;
            }
            
            _pathRecord = value;

            Save(this);
        }
    }
}
