using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour, IInputController
{
    private IRotatable _rotatable;
    private IControllable _person;

    void Awake()
    {
        GetComponent<SwipeDetector>().OnSwipe += OnSwipe;
    }

    void Start()
    {
        if(!TryGetComponent<IControllable>(out _person))
        {
            Debug.LogWarning("IControllable is null.");
        }

        if(!transform.GetChild(0).TryGetComponent<IRotatable>(out _rotatable))
        {
            Debug.LogWarning("IRotatable is null.");
        }
    }

    void FixedUpdate()
    {
        ProcessPcInput();
    }

    #region InputProcessing

    private void OnSwipe(SwipeData data)
    {
        if(data.Direction == SwipeDirection.Down)
        {
            Down();
        }
        if(data.Direction == SwipeDirection.Up)
        {
            Up();
        }
        if(data.Direction == SwipeDirection.Left)
        {
            Left();
        }
        if(data.Direction == SwipeDirection.Right)
        {
            Right();
        }
    }

    private void ProcessPcInput()
    {
        if(Input.GetKey("up"))
        {
            Up();
        }
        if(Input.GetKey("down"))
        {
            Down();
        }
        if(Input.GetKey("left"))
        {
            Left();
        }
        if(Input.GetKey("right"))
        {
            Right();
        }
    }

    #endregion

    #region IInputController

    public void Up()
    {
        if(_rotatable == null)
        {
            return;
        }

        _rotatable.TurnForward();
        _person.OnInput();
    }

    public void Down()
    {
        if(_rotatable == null)
        {
            return;
        }

        _rotatable.TurnBackward();
        _person.OnInput();
    }

    public void Left()
    {
        if(_rotatable == null)
        {
            return;
        }

        _rotatable.TurnLeft();
        _person.OnInput();
    }

    public void Right()
    {
        if(_rotatable == null)
        {
            return;
        }

        _rotatable.TurnRight();
        _person.OnInput();
    }

    #endregion
}