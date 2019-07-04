using System.Collections;
using UnityEngine;

public class PlayerController : GenericController
{
    #region Variables

    [SerializeField]
    private GameStateEventObj _GameStateObj;

    [SerializeField]
    private Rigidbody _RigidBody;

    public Rigidbody RigidBody { get { return _RigidBody; } }

    private bool _HasEnded;

    private bool _HasStarted;

    private float _CautiousTimer = 4;
    private float _Timer = 0;

    [SerializeField]
    private bool _CautiousAI;

    private bool _IsWaiting;

    [SerializeField]
    private bool _PlayerControlled;

    [SerializeField]
    private bool _LogTarget;

    [SerializeField]
    private int _ScoreCombo;

    [SerializeField]
    private int _TotalScoreCombo;

    [SerializeField]
    private bool _EndMove;

    [SerializeField]
    private GameObject _StarVFX;

    [SerializeField]
    private AudioSource _AudioReward;

    #endregion Variables

    #region Init

    private void Start()
    {
        _PlayerView.InjectAnim(_PlayerViewAnim);
        _PlayerViewAnim.GetAnimator().Play(AnimConstants.ANIM_READY);
        _PlayerView.TargetReachedEvent.AddListener(() =>
        {
            var temp = _PlayerModel.GetNextNode();

            if (temp == null)
            {
                if (_HasEnded)
                    return;
                _HasEnded = true;
                if (_PlayerControlled)
                    _GameStateObj.ChangeState.Invoke(GameStates.Win);
                else
                    _GameStateObj.ChangeState.Invoke(GameStates.Lose);

                return;
            }
            if (temp.name == Constants.COLLISION_RIGHT)
                _PlayerView.TurnOnReach(Turn.Right);
            if (temp.name == Constants.COLLISION_LEFT)
                _PlayerView.TurnOnReach(Turn.Left);
            _PlayerView.TargetPosEvent.Invoke(temp.position);
        });

        _GameStateObj.ChangeState.AddListener(_ =>
        {
            switch (_)
            {
                case GameStates.Start:
                    _PlayerView.SpeedEvent.Invoke(_PlayerModel.GetSpeed());
                    _PlayerView.TargetPosEvent.Invoke(_PlayerModel.CurrentNode.position);
                    _PlayerModel.UpdateState(CharacterStates.Idle);
                    _PlayerViewAnim.GetAnimator().Play(AnimConstants.ANIM_IDLE);
                    _HasStarted = true;
                    break;

                case GameStates.Results:
                    _EndMove = true;
                    break;

                case GameStates.Win:
                    ResetCombiMeter();
                    _EndMove = true;
                    break;

                case GameStates.Lose:
                    _EndMove = true;
                    break;
            }
        });
    }

    #endregion Init

    #region Update

    public void FixedUpdate()
    {
        if (!_HasStarted || _EndMove)
            return;
        if (_PlayerControlled)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
            {
                RunCommand(true);
            }
            else
            {
                if (_PlayerView.IsDeadFlag == false)
                    ResetCombiMeter();
                RunCommand(false);
            }
        }
        else
        {
            if (_CautiousAI)
            {
                _Timer += Time.deltaTime;
                if (!_IsWaiting)
                {
                    RunCommand(true);
                    if (_Timer > _CautiousTimer)
                    {
                        _Timer = 0;
                        _IsWaiting = true;
                    }
                }
                else
                {
                    RunCommand(false);
                    if (_Timer > 2)
                    {
                        _Timer = 0;
                        _IsWaiting = false;
                    }
                }
            }
            else
            {
                RunCommand(true);
            }
        }
    }

    #endregion Update

    #region Public Methods

    public void AddParent(Transform parent)
    {
        transform.parent = parent;
    }

    public void Kill(Vector3 force)
    {
        GetComponent<PlayerController>().RigidBody.constraints = RigidbodyConstraints.None;
        GetComponent<PlayerController>().RigidBody.AddForce(force);

        _PlayerView.TrailParticle.SetActive(true);
        _RigidBody.useGravity = true;
        _PlayerViewAnim.GetAnimator().Play(AnimConstants.ANIM_DIE);

        _PlayerView.DeadEvent.Invoke(true);
    }

    public void KillOnSpot()
    {
        _RigidBody.useGravity = false;
        _PlayerViewAnim.GetAnimator().SetFloat(AnimConstants.ANIM_DIE, Random.Range(1, 4));
        _PlayerViewAnim.GetAnimator().Play(AnimConstants.ANIM_DIE);
        KillParameters();
    }

    public void Flatten()
    {
        _PlayerView.SmokeParticle.SetActive(true);
        _PlayerViewAnim.GetAnimator().Play(AnimConstants.ANIM_FLATTEN);
        KillParameters();
    }

    #endregion Public Methods

    #region Private Methods

    private void RunCommand(bool ifRun)
    {
        _PlayerView.RunEvent.Invoke(ifRun);
        _PlayerViewAnim.SetTargetSpeed(ifRun ? 1 : 0);
    }

    private void KillParameters()
    {
        Collider.enabled = false;
        GetComponent<PlayerController>().RigidBody.constraints = RigidbodyConstraints.FreezeAll;
        _PlayerView.DeadEvent.Invoke(true);
    }

    private void ResetCombiMeter()
    {
        if (_ScoreCombo > 1)
        {
            _TotalScoreCombo += _ScoreCombo;
            Factory.Get<UIHandler>().AddTotalScoreCombo(_TotalScoreCombo);
        }
        _ScoreCombo = 0;
        Factory.Get<UIHandler>().AddScoreCombo(_ScoreCombo);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_PlayerControlled)
        {
            if (other.gameObject.name == Constants.SURPASS)
            {
                _ScoreCombo++;
                StartCoroutine(VFXStart());
                Factory.Get<UIHandler>().AddScoreCombo(_ScoreCombo);
            }
            if (other.gameObject.name == Constants.CAM_LEFT)
            {
                _PlayerView
                    ._EnableLeftCam.SetActive(true
                    );
            }
            if (other.gameObject.name == Constants.CAM_RIGHT)
            {
                _PlayerView
                    ._EnableRigthCam
                    .SetActive(true
                    );
            }
        }
    }

    private IEnumerator VFXStart()
    {
        _AudioReward.Play();
        _StarVFX.SetActive(true);
        yield return new WaitForSeconds(.5f);
        _StarVFX.SetActive(false);
    }

    #endregion Private Methods
}