﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.UI;

public class UIBinding : MonoBehaviour
{
    public string Binding;
    public string TargetType;
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

        if(!string.IsNullOrEmpty(Default))
        {
            SetValue(Default);
        }

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

        object valueToSet = value;

        if(!string.IsNullOrEmpty(ConvertType))
        {
            Type type = Type.GetType(ConvertType);
            if(type == null)
            {
                Debug.LogWarning(gameObject.name + " contains incorrect ConvertType: " + ConvertType);
                return;
            }
            
            valueToSet = Convert.ChangeType(value, type);
        }

        try
        {
            _target.GetType().GetProperty(Path).SetValue(_target, ConvertValue(valueToSet));
        }
        catch (Exception ex)
        {
            if(Notifications)
            {
                Debug.Log("Cannot access removed object or property: " + TargetType);
                Debug.Log("Details: \r\n" + ex.Message + "\r\n" + ex.StackTrace);
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

            SetValue(value);
        }
        else
        {
            if(!string.IsNullOrEmpty(Default))
            {
                SetValue(Default);
            }
        }
    }

    private object ConvertValue(object value)
    {
        if(!string.IsNullOrEmpty(Converter))
        {
            Type type = Type.GetType(Converter);
            if(type == null)
            {
                Debug.LogWarning(gameObject.name + " contains incorrect ConvertType: " + ConvertType);
                return value;
            }

            object converter = Activator.CreateInstance(type);
            if(converter == null)
            {
                Debug.LogWarning(gameObject.name + " contains incorrect Converter: " + Converter);
                return value;
            }

            MethodInfo method = type.GetMethod("Convert");
            value = method.Invoke(converter, new object[] { value });
        }
        return value;
    }

}