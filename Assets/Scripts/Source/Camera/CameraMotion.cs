using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMotion : MonoBehaviour, ITracker
{
    public float MaximumDuration = 10;
    public float DefaultDuration = 5;
    public float MinimumDuration = 2;
    public float MaximumDistanceToPlayer = 3;
    public int PlayerKillDistance = 5;
    public int FramesUpdateInterval = 10;
    public bool ReverseDirection = true;

    private PathTracker _playerPath;
    private IPersonController _personController;
    private int _pastFrameCount = 0;

    public int Count()
    {
        return -(int)transform.position.z;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if(!player.TryGetComponent<PathTracker>(out _playerPath))
        {
            Debug.LogWarning("PathTracker is null.");
        }
        
        if(!player.TryGetComponent<IPersonController>(out _personController))
        {
            Debug.LogWarning("IPersonController is null.");
        }
    }

    void FixedUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if(_playerPath.Count() + PlayerKillDistance < Count())
        {
            _personController.Die();
        }

        if(Time.frameCount - _pastFrameCount > FramesUpdateInterval)
        {
            transform.DOMove(GetDirection(), GetDuration());
            _pastFrameCount = Time.frameCount;
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        
        if(ReverseDirection)
        {
            direction = -direction;
        }

        return transform.position + direction;
    }

    private float GetDuration()
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
        return duration;
    }
}
