using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camera : MonoBehaviour, ITracker
{
    public float MaximumDuration = 10;
    public float DefaultDuration = 5;
    public float MinimumDuration = 2;
    public float MaximumDistanceToPlayer = 3;
    public bool ReverseDirection = true;

    private ITracker _playerPath;

    public int Count()
    {
        return -(int)transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerPath = player.GetComponent<ITracker>();
    }

    void FixedUpdate()
    {
        float duration = 10;
        ITracker cameraPath = this;

        if(cameraPath.Count() + MaximumDistanceToPlayer < _playerPath.Count())
        {
            duration = MinimumDuration;
        }
        else
        {
            if(cameraPath.Count() >= _playerPath.Count())
            {
                duration = MaximumDuration;
            }
            else
            {
                duration = DefaultDuration;
            }
        }

        Vector3 direction = transform.forward;
        
        if(ReverseDirection)
        {
            direction = -direction;
        }

        transform.DOMove(transform.position + direction, duration);
    }
}
