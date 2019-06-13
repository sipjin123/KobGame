using System.Collections;
using UnityEngine;

public class PendulumController : GenericObstacle
{
    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private CollisionHandler _RightCollision, _LeftCollision;

    [SerializeField]
    private bool _OverridePlayerParent;

    [SerializeField]
    private Vector3 _CollisionForce;

    [SerializeField]
    private Animator _Anim;

    [SerializeField]
    private GameObject _RightVFX, _LeftVFX;

    private void Start()
    {
        _Anim.speed = Random.Range(.5f, 1.5f);
        _RightCollision.CollidedObj.AddListener(_ =>
        {
            StartCoroutine(DelayVFXRight());
            Factory.Get<SFXManager>().PlaySFX(SFX.DiePendulum);
            Vector3 force = transform.right * _CollisionForce.x;
            force.y = _CollisionForce.y;
            force.z = _CollisionForce.z;
            _.GetComponent<PlayerController>().Kill(force);
            if (_.tag == Constants.PLAYER_TAG)
            {
                _GameStateObj.ChangeState.Invoke(GameStates.Results);
            }
        });
        _LeftCollision.CollidedObj.AddListener(_ =>
        {
            StartCoroutine(DelayVFXLeft());
            Factory.Get<SFXManager>().PlaySFX(SFX.DiePendulum);
            Vector3 force = -transform.right * _CollisionForce.x;
            force.y = _CollisionForce.y;
            force.z = _CollisionForce.z;
            _.GetComponent<PlayerController>().Kill(force);
            if (_.tag == Constants.PLAYER_TAG)
            {
                _GameStateObj.ChangeState.Invoke(GameStates.Results);
            }
        });
    }
    IEnumerator DelayVFXLeft()
    {
        _LeftVFX.SetActive(true);
        yield return new WaitForSeconds(.5f);
        _LeftVFX.SetActive(false);
    }
    IEnumerator DelayVFXRight()
    {
        _RightVFX.SetActive(true);
        yield return new WaitForSeconds(.5f);
        _RightVFX.SetActive(false);
    }
}