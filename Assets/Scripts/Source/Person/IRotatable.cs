using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRotatable
{
    void TurnLeft();
    void TurnRight();
    void TurnForward();
    void TurnBackward();
    Direction GetDirection();
}
