using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerViewAnimation : MonoBehaviour, IViewAnimation
{
    [SerializeField]
    Animator _Animator;

    public Animator GetAnimator()
    {
        return _Animator;
    }

}
