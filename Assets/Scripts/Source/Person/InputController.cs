using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private IRotatable _rotatable;
    private IControllable _person;

    void Start()
    {
        if(!TryGetComponent<IControllable>(out _person))
        {
            Debug.LogWarning("IControllable is null");
        }

        if(!TryGetComponent<IRotatable>(out _rotatable))
        {
            Debug.LogWarning("IRotatable is null");
        }
    }

    void FixedUpdate()
    {
        _rotatable.TurnRight();
        _person.OnInput();
    }
}
