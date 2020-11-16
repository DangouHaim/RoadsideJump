using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.UI;

public class UIBinding : MonoBehaviour
{
    public string TargetType;
    public string Binding;
    public string Path;
    public string ConvertType;
    public string Converter;
    public string Default;
    public bool Notifications = false;

    private Bindings _bindings;
    private object _target;

    void Awake()
    {
        _target = GetComponent(TargetType);

        SetValue(Default);

        _bindings = Bindings.GetInstance();
        _bindings.OnBindingUpdated += OnBindingChanged;
    }

    void Start()
    {
        UpdateValue(_bindings?.GetBinding(Binding)?.Data);
    }

    private void SetValue(object value)
    {
        if(_target == null)
        {
            return;
        }

        Type type = Type.GetType(ConvertType);
        if(type == null)
        {
            Debug.LogWarning(gameObject.name + " contains incorrect ConvertType: " + ConvertType);
            return;
        }
        
        object valueToSet = Convert.ChangeType(value, type);

        try
        {
            _target.GetType().GetProperty(Path).SetValue(_target, valueToSet);
        }
        catch
        {
            if(Notifications)
            {
                Debug.Log("Cannot access removed object: " + TargetType);
            }
        }
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
            // Convert value with Converter
            if(!string.IsNullOrEmpty(Converter))
            {
                Type type = Type.GetType(Converter);
                if(type == null)
                {
                    Debug.LogWarning(gameObject.name + " contains incorrect ConvertType: " + ConvertType);
                    return;
                }

                object converter = Activator.CreateInstance(type);
                if(converter == null)
                {
                    Debug.LogWarning(gameObject.name + " contains incorrect Converter: " + Converter);
                    return;
                }

                MethodInfo method = type.GetMethod("Convert");
                value = method.Invoke(converter, new object[] { value });
            }

            SetValue(value);
        }
        else
        {
            SetValue(Default);
        }
    }

}