using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Reflection;
using System;

public class UIBinding : MonoBehaviour
{
    public string TargetType;
    public string Binding;
    public string Path;
    public string ConvertType;
    public string Default;

    private Bindings _bindings;
    private object _target;

    void Awake()
    {
        _target = GetComponent(TargetType);

        SetValue(Default);

        _bindings = Bindings.GetInstance();
        _bindings.OnBindingUpdated += OnBindingChanged;
    }

    private void SetValue(object value)
    {
        Type type = Type.GetType(ConvertType);
        if(type == null)
        {
            Debug.LogWarning(gameObject.name + " contains incorrect ConvertType: " + ConvertType);
            return;
        }
        
        object valueToSet = Convert.ChangeType(value, type);

        _target.GetType().GetProperty(Path).SetValue(_target, valueToSet);
    }

    private void OnBindingChanged(object sender, Bindings.BindingEventArgs e)
    {
        if(e.Name == Binding)
        {
            UpdateValue(e.Binding.Data);
        }
    }

    private void UpdateValue(object value)
    {
        if(value != null)
        {
            SetValue(value);
        }
        else
        {
            SetValue(Default);
        }
    }

}