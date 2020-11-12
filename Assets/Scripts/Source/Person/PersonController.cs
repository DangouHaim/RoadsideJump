using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersonController : MonoBehaviour, IPersonController, IControllable
{
    #region Idle
    
    [Header("Idle")]
    public float IdleScaling = 0.1f;
    public float IdleDuration = 1;
    private bool _isPersonScaleUp = true;
    private bool _isIdleReady = true;

    #endregion

    #region Moving

    [Header("Moving")]
    public float JumpScaling = 0.2f;
    public float JumpScalingDuration = 0.2f;
    public float JumpScalingInterval = 0.4f;
    public float JumpForce = 1;
    public float JumpDistance = 1;
    public float JumpDuration = 1;
    public bool ReverseAxis = true;
    private bool _isJumpReady = true;

    #endregion

    #region Dead

    [Header("Dead")]
    public int RespawnTime = 10;

    #endregion

    #region Inner state
    
    private Vector3 _defaultScale;
    private PersonState _state;
    private IRotatable _rotatable;
    private GameObject _skin;

    #endregion

    #region Init
    void Awake()
    {
        DOTween.Init();
    }

    void Start()
    {
        _defaultScale = transform.localScale;
        
        _skin = transform.GetChild(0).gameObject;

        if(!_skin.TryGetComponent<IRotatable>(out _rotatable))
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
                    break;
                }
                case PersonState.Moving:
                {
                    break;
                }
                case PersonState.Dead:
                {
                    yield return new WaitForSeconds(RespawnTime);
                    Respawn();
                    break;
                }
                case PersonState.Respawned:
                {
                    Respawn();
                    break;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    #region IPersonController

    // Idle animation
    public void Idle()
    {
        if(!_isIdleReady)
        {
            return;
        }
        _isIdleReady = false;

        AnimateIdle();

        _isPersonScaleUp = !_isPersonScaleUp;
    }

    public void Move()
    {
        if(!_isJumpReady || _state == PersonState.Dead)
        {
            return;
        }
        _isJumpReady = false;

        DoJump(_rotatable);
    }
    
    public void Die()
    {
        if(_state != PersonState.Dead)
        {
            _state = PersonState.Dead;
            GetComponent<InputController>().enabled = false;
            transform.Find("Skin").gameObject.SetActive(false);
            transform.Find("DeadBody").gameObject.SetActive(true);
        }
    }

    public void Respawn()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Main");
    }

    #endregion

    #region  IControllable
    public void OnInput()
    {
        _state = PersonState.Moving;
        Move();
    }
    #endregion

    // Moving logic
    private void DoJump(IRotatable rotatable)
    {
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
        
        AnimateJump(direction);
    }

    private void AnimateIdle()
    {
        Sequence animation = DOTween.Sequence();
        
        animation.onComplete = () =>
        {
            _isIdleReady = true;
        };

        if(_isPersonScaleUp)
        {
            animation.Append(
                _skin.transform.DOScaleY(
                    _defaultScale.y + IdleScaling,
                    IdleDuration
                )
            );
        }
        else
        {
            animation.Append(
                _skin.transform.DOScaleY(
                    _defaultScale.y,
                    IdleDuration
                )
            );
        }
    }

    private void AnimateJump(Vector3 direction)
    {
        // Animation sequence

        Sequence jumpSequence = DOTween.Sequence();

        jumpSequence.onComplete = () =>
        {
            _isJumpReady = true;
            ResetState();
        };

        // Before jump
        jumpSequence.Append(
            _skin.transform.DOScaleY(
                _defaultScale.y - JumpScaling,
                JumpScalingDuration
            )
        );

        jumpSequence.AppendInterval(JumpScalingInterval);

        jumpSequence.Append(
            _skin.transform.DOScaleY(
                _defaultScale.y,
                JumpScalingDuration
            )
        );

        // Jump
        if(CanMove(direction))
        {
            jumpSequence.Append(
                transform.DOJump(
                    transform.position + direction,
                    JumpForce,
                    1,
                    JumpDuration
                )
            );
        }
    }

    private void ResetState()
    {
        if(_state != PersonState.Dead)
        {
            _state = PersonState.Idle;
        }
    }

    // Detect obstacles using raycast
    private bool CanMove(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity, 1))
        {
            if(hit.transform.tag == "Obstacle" && hit.distance < 1)
            {
                return false;
            }
        }
        return true;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }
}