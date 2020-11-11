using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotatable : MonoBehaviour, IRotatable
{
    public float RotationSpeed = 0.1f;
    private Direction _direction;

    public Direction GetDirection()
    {
        return _direction;
    }

    public void TurnBackward()
    {
        _direction = Direction.Backward;
        transform.DORotate(new Vector3(0, 180, 0), RotationSpeed);
    }

    public void TurnForward()
    {
        _direction = Direction.Forward;
        transform.DORotate(new Vector3(0, 0, 0), RotationSpeed);
    }

    public void TurnLeft()
    {
        _direction = Direction.Left;
        transform.DORotate(new Vector3(0, -90, 0), RotationSpeed);
    }

    public void TurnRight()
    {
        _direction = Direction.Right;
        transform.DORotate(new Vector3(0, 90, 0), RotationSpeed);
    }
}
