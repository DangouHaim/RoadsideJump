using UnityEngine;

public interface IMovable
{
    void MoveTo(Vector3 target, float duration, bool rotate = false);
}