using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolNotConverter : IConverter
{
    public object Convert(object value)
    {
        return !(bool)value;
    }
}