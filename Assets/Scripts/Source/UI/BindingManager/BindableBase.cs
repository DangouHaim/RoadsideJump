using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class BindableBase : AutoSaveBase
{
    [NonSerialized]
    private static Bindings _bindings = Bindings.GetInstance();

    public void OnPropertyChanged(string name, object value)
    {
        _bindings.UpdateBinding(name, value);
    }
}