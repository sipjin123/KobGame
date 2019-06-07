using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkViewAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator _Animator;


    public void SetReady()
    {
        _Animator.Play(AnimConstants.ANIM_READY);
    }
    public void SetIdle()
    {
        _Animator.Play(AnimConstants.ANIM_IDLE);
    }
    public void SetAttack()
    {
        _Animator.Play(AnimConstants.ANIM_ATTACK);
    }
}
