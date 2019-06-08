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

    private void Start()
    {
        _RightCollision.CollidedObj.AddListener(_ =>
        {
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
}