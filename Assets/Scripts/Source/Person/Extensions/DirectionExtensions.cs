using UnityEngine;

static class DirectionExtensions 
{
  public static Vector3 ToVector3(this Direction direction, Transform transform) 
  {
    switch(direction)
    {
        case Direction.Forward:
        {
            return transform.forward;
        }
        case Direction.Backward:
        {
            return -transform.forward;
        }
        case Direction.Left:
        {
            return -transform.right;
        }
        case Direction.Right:
        {
            return transform.right;
        }
        default:
        {
            return transform.position;
        }
    }
  }
}