using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PersonController : MonoBehaviour, IPersonController, IControllable
{
    #region Idle
    
    [Header("Idle")]
    public float IdleScaling = 0.1f;
    public float IdleDuration = 1;
    public float IdleFrequency = 1;
    private bool _isPersonScaleUp = true;

    #endregion

    #region Moving

    [Header("Moving")]
    public float JumpScaling = 0.2f;
    public float JumpForce = 1;
    public float JumpDistance = 1;
    public float JumpDuration = 1;
    public float JumpFrequency = 1;
    public bool ReverseAxis = true;

    #endregion

    #region Inner state
    
    private Vector3 _defaultScale;
    private PersonState _state;
    private IRotatable _rotatable;

    #endregion

    #region Init
    void Awake()
    {
        DOTween.Init();
    }

    void Start()
    {
        _defaultScale = transform.localScale;
        
        if(!TryGetComponent<IRotatable>(out _rotatable))
        {
            Debug.LogWarning("IRotatable is null");
        }

        StartCoroutine("Controller");
    }
    #endregion

    IEnumerator Controller()
    {   
        while(true)
        {
            switch(_state)
            {
                case PersonState.Idle:
                {
                    Idle();
                    yield return new WaitForSeconds(IdleFrequency);
                    break;
                }
                case PersonState.Moving:
                {
                    Move();
                    yield return new WaitForSeconds(JumpFrequency);
                    break;
                }
                case PersonState.Dead:
                {
                    Die();
                    break;
                }
                case PersonState.Respawned:
                {
                    Respawn();
                    break;
                }
            }
            ResetState();
            yield return new WaitForFixedUpdate();
        }
    }

    #region IPersonController

    // Idle animation
    public void Idle()
    {
        if(_isPersonScaleUp)
            {
                transform.DOScaleY(
                    _defaultScale.y + IdleScaling,
                    IdleDuration
                );
            }
            else
            {
                transform.DOScaleY(
                    _defaultScale.y,
                    IdleDuration
                );
            }

            _isPersonScaleUp = !_isPersonScaleUp;
    }

    public void Move()
    {
        DoJump(_rotatable);
    }
    
    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Respawn()
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region  IControllable
    public void OnInput()
    {
        _state = PersonState.Moving;
    }
    #endregion

    // Moving logic
    private void DoJump(IRotatable rotatable)
    {
        // Prepare to jump

        // Detect jump direction
        Vector3 direction = rotatable.GetDirection().ToVector3(transform);

        // Reverse axis direction if model/prefab axis reversed
        if(ReverseAxis)
        {
            direction *= -JumpDistance;
        }
        else
        {
            direction *= JumpDistance;
        }
        
        transform.DOJump(
            transform.position + direction,
            JumpForce,
            1,
            JumpDuration
        );
    }

    private void ResetState()
    {
        _state = PersonState.Idle;
    }
}
