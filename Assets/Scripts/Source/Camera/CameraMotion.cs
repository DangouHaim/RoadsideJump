using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMotion : MonoBehaviour, ITracker
{
    public float MaximumSpeed = 4;
    public float DefaultSpeed = 2;
    public float MinimumSpeed = 0.5f;
    public float MaximumDistanceToPlayer = 3;
    public int PlayerKillDistance = 4;
    public bool ReverseDirection = true;

    private PathTracker _playerPath;
    private IPersonController _personController;

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

        transform.Translate(GetDirection() * Time.deltaTime * GetSpeed());
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;
        
        if(ReverseDirection)
        {
            direction = -direction;
        }

        return direction;
    }

    private float GetSpeed()
    {
        float speed = 10;
        ITracker cameraPath = this;

        if(cameraPath.Count() + MaximumDistanceToPlayer < _playerPath.Count())
        {
            speed = MaximumSpeed;
        }
        else
        {
            if(cameraPath.Count() >= _playerPath.Count())
            {
                speed = MinimumSpeed;
            }
            else
            {
                speed = DefaultSpeed;
            }
        }
        return speed;
    }
}
