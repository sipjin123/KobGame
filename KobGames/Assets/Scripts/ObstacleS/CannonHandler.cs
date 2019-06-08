using UnityEngine;

public class CannonHandler : GenericObstacle
{
    [SerializeField]
    private Transform _SpawnPoint;

    [SerializeField]
    private GameObject _CannonBall;

    [SerializeField]
    private Rigidbody _CannonRigid;

    [SerializeField]
    private CollisionHandler _CollisionHandler;

    [SerializeField]
    private float _Cooldown = 4;

    private float _Timer;

    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private GameObject _WarningVFX, _ShotVFX;

    private void Start()
    {
        _Cooldown = Random.Range(2, 5);
        _CollisionHandler.CollidedObj.AddListener(_ =>
        {
            _.GetComponent<PlayerController>().Kill(transform.right * 1000);
            if (_.tag == Constants.PLAYER_TAG)
                _GameStateObj.ChangeState.Invoke(GameStates.Results);
        });
    }

    private void Update()
    {
        if (_Timer < _Cooldown)
        {
            _Timer += Time.deltaTime;
            if (_Timer > 1)
            {
                _ShotVFX.SetActive(false);
            }
            if (_Timer < _Cooldown - .5f)
            {
                _WarningVFX.SetActive(true);
            }
        }
        else
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        _ShotVFX.SetActive(true);
        _WarningVFX.SetActive(false);
        _Timer = 0;
        _CannonRigid.velocity = new Vector3(0, 0, 0);
        var force = transform.right * 1000;
        _CannonBall.transform.position = _SpawnPoint.position;
        _CannonBall.SetActive(true);
        _CannonRigid.AddForce(force);
    }
}