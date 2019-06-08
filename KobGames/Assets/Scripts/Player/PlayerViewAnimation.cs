using UnityEngine;

public class PlayerViewAnimation : MonoBehaviour, IViewAnimation
{
    [SerializeField]
    private Animator _Animator;

    private float _lerpTimer = 0;
    private bool _ifReverse = false;

    public Animator GetAnimator()
    {
        return _Animator;
    }

    public void SetTargetSpeed(float speed)
    {
        _ifReverse = speed == 0 ? false : true;
        _lerpTimer = _ifReverse ? 1 : 0;
    }

    private void FixedUpdate()
    {
        if (_ifReverse)
        {
            if (_lerpTimer > 0)
            {
                _lerpTimer -= Time.deltaTime;
                _Animator.SetFloat(AnimConstants.SPEED, _lerpTimer);
            }
        }
        else
        {
            if (_lerpTimer < 1)
            {
                _lerpTimer += Time.deltaTime;
                _Animator.SetFloat(AnimConstants.SPEED, _lerpTimer);
            }
        }
    }
}