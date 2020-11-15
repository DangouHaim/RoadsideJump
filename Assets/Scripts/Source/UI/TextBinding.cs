using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBinding : MonoBehaviour
{
    public string Binding;
    public string Default;

    private Bindings _bindings;
    private TextMeshProUGUI _text;

    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = Default;

        _bindings = Bindings.GetInstance();
        _bindings.OnBindingUpdated += OnBindingChanged;
    }

    private void OnBindingChanged(object sender, Bindings.BindingEventArgs e)
    {
        if(e.Name == Binding)
        {
            UpdateText(e.Binding.Data);
        }
    }

    private void UpdateText(object value)
    {
        if(value != null)
        {
            _text.text = value.ToString();
        }
        else
        {
            _text.text = Default;
        }
    }

}