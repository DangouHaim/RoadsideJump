using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Train : MonoBehaviour, IMovable
{
    public void MoveTo(Vector3 target, float duration, bool rotate = false)
    {
        if(rotate)
        {
            transform.DORotate(new Vector3(0, 180, 0), 0.3f);
        }
        
        transform.DOMove(target, duration);
    }
}