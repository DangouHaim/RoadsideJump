using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour, IRotatable
{
    private Direction _direction;

    public Direction GetDirection()
    {
        return _direction;
    }

    public void TurnBackward()
    {
        _direction = Direction.Backward;
    }

    public void TurnForward()
    {
        _direction = Direction.Forward;
    }

    public void TurnLeft()
    {
        _direction = Direction.Left;
    }

    public void TurnRight()
    {
        _direction = Direction.Right;
    }
}
