using System.Collections;
using UnityEngine;

public class CannonHandler : GenericObstacle
{
    [SerializeField]
    private Transform[] _SpawnPoint;

    [SerializeField]
    private GameObject[] _CannonBall;

    [SerializeField]
    private Rigidbody[] _CannonRigid;

    [SerializeField]
    private CollisionHandler[] _CollisionHandler;

    [SerializeField]
    private float _Cooldown = 4;

    private float _Timer;

    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private GameObject[] _WarningVFX, _ShotVFX;

    [SerializeField]
    private AudioSource _Audio;

    private void Start()
    {
        _Cooldown = Random.Range(3, 5);
        for (int i = 0; i < _CollisionHandler.Length; i++)
        {
            _CollisionHandler[i].CollidedObj.AddListener(_ =>
            {
                Factory.Get<SFXManager>().PlaySFX(SFX.DieCannon);
                _.GetComponent<PlayerController>().Kill(transform.right * 1000);
                if (_.tag == Constants.PLAYER_TAG)
                    _GameStateObj.ChangeState.Invoke(GameStates.Results);
            });
        }
        Shoot();
    }

    private void Update()
    {
        if (_Timer < _Cooldown)
        {
            _Timer += Time.deltaTime;
            if (_Timer > 1)
            {
                for (int i = 0; i < _SpawnPoint.Length; i++)
                {
                    _ShotVFX[i].SetActive(false);
                }
            }

            if (_Timer > _Cooldown - 1f)
            {
                for (int i = 0; i < _SpawnPoint.Length; i++)
                {
                    _WarningVFX[i].SetActive(true);
                }
            }
        }
        else
        {
            _Timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        _Audio.Play();

        for (int i = 0; i < _SpawnPoint.Length; i++)
        {
            _CannonRigid[i].Sleep();
            _ShotVFX[i].SetActive(true);
            _WarningVFX[i].SetActive(false);
            var force = _SpawnPoint[i].forward * 1000;
            _CannonBall[i].transform.position = _SpawnPoint[i].position;
            _CannonRigid[i].AddForce(force);
            _CannonBall[i].SetActive(true);
        }
        StartCoroutine(DelayShoot());
    }

    private IEnumerator DelayShoot()
    {
        yield return new WaitForSeconds(.2f);
    }
}