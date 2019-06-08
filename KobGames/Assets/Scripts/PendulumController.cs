using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumController : MonoBehaviour
{
    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private CollisionHandler _RightCollision, _LeftCollision;

    [SerializeField]
    bool _OverridePlayerParent;
    private void Start()
    {
        _RightCollision.CollidedObj.AddListener(_ =>
        {
            Vector3 force = transform.right * 500;
            force.y = 100;
            force.z = 100;
            _.GetComponent<PlayerController>().Kill(force);
            if (_.tag == Constants.PLAYER_TAG)
            {
                _GameStateObj.ChangeState.Invoke(GameStates.Results);
            }
        });
        _LeftCollision.CollidedObj.AddListener(_ =>
        {
            Vector3 force = -transform.right * 500;
            force.y = 100;
            force.z = 100;
            _.GetComponent<PlayerController>().Kill(force);
            if (_.tag == Constants.PLAYER_TAG)
            {
                _GameStateObj.ChangeState.Invoke(GameStates.Results);
            }
        });
    }
}
