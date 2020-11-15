using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Bindings
{
    public class Binding
    {
        public object Data { get; set; }
    }

    public class BindingEventArgs : EventArgs
    {
        public string Name { get; set; }
        public Binding Binding { get; set; }
    }

    private event EventHandler<BindingEventArgs> OnPorpertyChanged = (s, e) => {};
    public event EventHandler<BindingEventArgs> OnBindingUpdated = (s, e) => {};

    private Dictionary<string, Binding> _bindings;

    private static Bindings _instance = null;
    private static object _lock = new object();

    private Bindings()
    {
        _bindings = new Dictionary<string, Binding>();
        OnPorpertyChanged += PorpertyChanged;
    }

    public static Bindings GetInstance()
    {
        if(_instance == null)
        {
            lock(_lock)
            {
                if(_instance == null)
                {
                    _instance = new Bindings();
                }
            }
        }

        return _instance;
    }

    private void PorpertyChanged(object sender, BindingEventArgs e)
    {
        if(_bindings.Keys.Contains(e.Name))
        {
            _bindings[e.Name] = e.Binding;
        }
        else
        {
            _bindings.Add(e.Name, e.Binding);
        }

        OnBindingUpdated.Invoke(this, e);
    }

    public void UpdateBinding(string name, object value)
    {
        OnPorpertyChanged.Invoke(
            this,
            new Bindings.BindingEventArgs()
            {
                Name = name,
                Binding = new Bindings.Binding() { Data = value }
            }
        );
    }

    public Binding GetBinding(string name)
    {
        if(!_bindings.Keys.Contains(name))
        {
            return null;
        }

        return _bindings[name];
    }

}