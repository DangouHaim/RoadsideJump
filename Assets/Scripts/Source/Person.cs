using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Person : MonoBehaviour
{
    public float IdleAnimationForce = 0.1f;
    public float IdleAnimationDuration = 1;
    private bool _isPersonScaleUp = true;

    private Vector3 _defaultScale;

    #region Init
    void Awake()
    {
        DOTween.Init();
    }

    void Start()
    {
        _defaultScale = transform.localScale;
        StartCoroutine("Controller");
    }
    #endregion

    IEnumerator Controller()
    {   
        while(true)
        {
            Idle();

            yield return new WaitForSeconds(1);
        }
    }

    // Idle animation
    private void Idle()
    {
        if(_isPersonScaleUp)
            {
                transform.DOScaleY(
                    _defaultScale.y + IdleAnimationForce,
                    IdleAnimationDuration
                );
            }
            else
            {
                transform.DOScaleY(
                    _defaultScale.y,
                    IdleAnimationDuration
                );
            }

            _isPersonScaleUp = !_isPersonScaleUp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
