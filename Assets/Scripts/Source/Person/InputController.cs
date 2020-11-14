using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour, IInputController
{
    private IRotatable _rotatable;
    private IControllable _person;
    private SwipeController _swipeController;

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

        if(!transform.TryGetComponent<SwipeController>(out _swipeController))
        {
            Debug.LogWarning("SwipeController is null.");
        }
    }

    void Update()
    {
        ProcessMobileInput();
    }

    void FixedUpdate()
    {
        ProcessPcInput();
    }

    #region InputProcessing

    private void ProcessMobileInput()
    {
        Direction swipe = _swipeController.GetSwipeDirection();
        
        switch(swipe)
        {
            case Direction.Forward:
            {
                Up();
                break;
            }
            case Direction.Backward:
            {
                Down();
                break;
            }
            case Direction.Left:
            {
                Left();
                break;
            }
            case Direction.Right:
            {
                Right();
                break;
            }
            case Direction.Empty:
            {
                break;
            }

            default:
            {
                break;
            }
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
        _rotatable.TurnForward();
        _person.OnInput();
    }

    public void Down()
    {
        _rotatable.TurnBackward();
        _person.OnInput();
    }

    public void Left()
    {
        _rotatable.TurnLeft();
        _person.OnInput();
    }

    public void Right()
    {
        _rotatable.TurnRight();
        _person.OnInput();
    }

    #endregion
}
