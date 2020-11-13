using System;

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

    private int _path;
    public int Path
    {
        get { return _path; }
        set
        {
            if(_path == value)
            {
                return;
            }
            
            _path = value;

            Save(this);
        }
    }
}
