using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class PersonController : MonoBehaviour, IPersonController, IControllable
{
    #region DeathZone

    [Header("Death Zone")]
    public float DeathDistance = 10;

    #endregion

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
    private PathTracker _pathTracker;
    private bool _isDrawnSafe = false;

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

        if(!TryGetComponent<PathTracker>(out _pathTracker))
        {
            Debug.LogWarning("PathTracker is null.");
        }

        _pathTracker.RecordAchived += (s, e) =>
        {
            // Make effect on record achived
            DeadBodyPart.UseGravity = false;
        };

        // Record not achived, disable effect
        DeadBodyPart.UseGravity = true;

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
                case PersonState.Drawn:
                {
                    transform.DOMove(transform.position - transform.up * 1.2f, 0.1f);
                    yield return new WaitForSeconds(0.2f);
                    Die();
                    break;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(transform.position.x) > DeathDistance)
        {
            Die();
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

    public bool IsAlive()
    {
        return _state != PersonState.Dead;
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

    private void DoSwim(GameObject swimObject)
    {
        _isDrawnSafe = true;
        transform.parent = swimObject.transform;
        AnimateLog(swimObject);
    }

    private void ReleaseLog()
    {
        _isDrawnSafe = false;
        if(transform.parent != null)
        {
            transform.parent = null;
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

    private void DrawnToDie()
    {
        GetComponent<InputController>().enabled = false;
        _state = PersonState.Drawn;
    }

    void OnTriggerEnter(Collider collider)
    {
        // Swimming on log
        if(collider.gameObject.tag == "Log")
        {
            DoSwim(collider.gameObject);
        }
        if(collider.gameObject.tag == "Enemy")
        {
            Die();
        }
        if(!_isDrawnSafe && collider.gameObject.tag == "Water")
        {
            DrawnToDie();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Log")
        {
            ReleaseLog();
        }
    }
}