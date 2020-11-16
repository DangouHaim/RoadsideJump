using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConverter
{
    object Convert(object value);
}