using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingsModel : BindableBase
{
    private bool _useSound;
    public bool UseSound
    {
        get
        {
            return _useSound;
        }
        set
        {
            if(value == _useSound)
            {
                return;
            }
            _useSound = value;
            OnPropertyChanged("UseSound", _useSound);
            Save(this);
        }
    }

    public SettingsModel()
    {
        
    }

    public SettingsModel(SettingsModel source)
    {
        UseSound = source.UseSound;
    }
}
