using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingModel : BindableBase
{
    private bool _loading;
    public bool Loading
    {
        get
        {
            return _loading;
        }
        set
        {
            if(value == _loading)
            {
                return;
            }
            _loading = value;
            OnPropertyChanged("Loading", _loading);
        }
    }
}
