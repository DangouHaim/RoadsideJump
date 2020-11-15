using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BindableBase
{
    private Bindings _bindings;

    public BindableBase()
    {
        _bindings = Bindings.GetInstance();
    }

    public void OnPropertyChanged(string name, object value)
    {
        _bindings.UpdateBinding(name, value);
    }
}