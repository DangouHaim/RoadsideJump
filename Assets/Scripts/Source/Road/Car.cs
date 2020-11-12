using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Car : MonoBehaviour, IMovable
{
    void Start()
    {
        ActiveRandomSkin();
    }

    private void ActiveRandomSkin()
    {
        int index = Random.Range(0, transform.childCount - 1);

        int i = 0;
        foreach(Transform child in transform)
        {
            if(i == index)
            {
                child.gameObject.SetActive(true);
                break;
            }
            i++;
        }
    }

    public void MoveTo(Vector3 target, float duration, bool rotate = false)
    {
        if(rotate)
        {
            transform.DORotate(new Vector3(0, 180, 0), 1);
        }
        
        transform.DOMove(target, duration);
    }
}
