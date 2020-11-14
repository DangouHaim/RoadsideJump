using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class PersonController
{
    private void AnimateLog(GameObject swimObject, float duration = 0.2f)
    {
        // Animate
        Sequence s = DOTween.Sequence();
        
        Transform log = swimObject.transform;
        float originY = log.position.y;
        
        s.Append(log.DOMoveY(originY - 0.1f, duration));
        s.Append(log.DOMoveY(originY, duration));
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
}
