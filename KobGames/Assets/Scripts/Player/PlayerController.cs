using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GenericController
{
    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private Rigidbody _RigidBody;
    public Rigidbody RigidBody { get { return _RigidBody; } }

    [SerializeField]
    private bool _PlayerControlled;
    public void FixedUpdate()
    {
        if (_PlayerControlled)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _PlayerView.RunEvent.Invoke(true);
                _PlayerViewAnim.SetTargetSpeed(1);
            }
            else
            {
                _PlayerView.RunEvent.Invoke(false);
                _PlayerViewAnim.SetTargetSpeed(0);
            }
        }
    }

    public void AddParent(Transform parent)
    {
        transform.parent = parent;
    }

    public void Kill(Vector3 force)
    {
        Collider.enabled = false;
        _RigidBody.useGravity = true;
        _PlayerViewAnim.GetAnimator().Play(AnimConstants.ANIM_DIE);
        _PlayerView.DeadEvent.Invoke(true);
        GetComponent<PlayerController>().RigidBody.constraints = RigidbodyConstraints.None;
        GetComponent<PlayerController>().RigidBody.AddForce(force);

    }

    public void KillOnSpot( )
    {
        Collider.enabled = false;
        _RigidBody.useGravity = false;
        _PlayerViewAnim.GetAnimator().Play(AnimConstants.ANIM_DIE);
        _PlayerView.DeadEvent.Invoke(true);
    }

    void Start()
    {
        _PlayerView.TargetReachedEvent.AddListener(() =>
        {
            var temp = _PlayerModel.GetNextNode();
            if(temp == null)
            {
                Debug.LogError("End of GAme");
                return;
            }
            _PlayerView.TargetPosEvent.Invoke(temp.position);
        });

        _GameStateObj.ChangeState.AddListener(_ =>
        {
            switch(_)
            {
                case GameStates.Start:
                    _PlayerView.SpeedEvent.Invoke(_PlayerModel.GetSpeed());
                    _PlayerView.TargetPosEvent.Invoke(_PlayerModel.CurrentNode.position);
                    _PlayerModel.UpdateState(CharacterStates.Idle);
                    break;
                case GameStates.Results:
                    break;
            }
        });
    }
}
