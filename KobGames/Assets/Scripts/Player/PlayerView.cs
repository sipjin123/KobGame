using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour, IView
{
    #region Variables

    [SerializeField]
    private Transform _MovableTransform;

    [SerializeField]
    private ParticleSystem _RunParticles;

    [SerializeField]
    private bool _IfMainPlayer;

    private Vector3 leftTarget;

    [SerializeField]
    public GameObject _EnableLeftCam, _EnableRigthCam;

    [SerializeField]
    private bool _LogTargetReach;

    public BoolEvent RunEvent = new BoolEvent();
    public BoolEvent DeadEvent = new BoolEvent();
    public FloatEvent SpeedEvent = new FloatEvent();
    public Vector3Event TargetPosEvent = new Vector3Event();
    public UnityEvent TargetReachedEvent = new UnityEvent();

    private PlayerViewAnimation _PlayerAnim;

    [SerializeField]
    private bool _RunFlag;

    [SerializeField]
    private bool _IsDeadFlag;

    public bool IsDeadFlag { get { return _IsDeadFlag; } }

    [SerializeField]
    private Vector3 _TargetNode;

    [SerializeField]
    private float _Speed;

    private float _RotDamp;

    [SerializeField]
    private GameObject _KnockOutVFX;

    [SerializeField]
    private GameObject _SmokeParticle;

    public GameObject SmokeParticle { get { return _SmokeParticle; } }

    [SerializeField]
    private GameObject _TrailParticle;

    public GameObject TrailParticle { get { return _TrailParticle; } }

    private bool _StartTurning;
    private float _TurnMagnitude = 90;
    private bool _TurnOnReach;

    private Turn _TurnTo;
    public Turn TurnTo { get { return _TurnTo; } }

    #endregion Variables

    #region Init

    private void Start()
    {
        _TargetNode = (_MovableTransform.position + transform.forward * 5);
        SpeedEvent.AddListener(_ => _Speed = _);
        RunEvent.AddListener(_ => _RunFlag = _);
        DeadEvent.AddListener(_ =>
        {
            _IsDeadFlag = _;
            if (_IsDeadFlag)
                _KnockOutVFX.SetActive(true);
        });
        TargetPosEvent.AddListener(_ =>
        {
            _RotDamp = 0;
            _TargetNode = _;
        });
    }

    #endregion Init

    #region Public Methods

    public void InjectAnim(PlayerViewAnimation anim)
    {
        _PlayerAnim = anim;
    }

    public Transform GetTransform()
    {
        return _MovableTransform;
    }

    public void TurnOnReach(Turn direction)
    {
        _TurnOnReach = true;
        _TurnTo = direction;
    }

    #endregion Public Methods

    #region Update

    private void FixedUpdate()
    {
        if (_StartTurning)
        {
            _RotDamp += Time.deltaTime * 2;

            var lookPos = leftTarget - _MovableTransform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            _MovableTransform.rotation = Quaternion.Slerp(_MovableTransform.rotation, rotation, _RotDamp);

            if (_RotDamp >= 1.2f)
            {
                _StartTurning = false;
            }
        }

        if (!_RunFlag || _IsDeadFlag || _StartTurning)
        {
            _RunParticles.enableEmission = false;
            return;
        }

        _RunParticles.enableEmission = true;

        if (_TurnOnReach)
        {
            if (Vector3.Distance(_MovableTransform.position, _TargetNode) < 3.2f)
            {
                if (_TurnTo == Turn.Right)
                {
                    if (_IfMainPlayer)
                        _EnableRigthCam.SetActive(true);
                }
                else
                {
                    if (_IfMainPlayer)
                        _EnableLeftCam.SetActive(true);
                }
            }
        }

        if (Vector3.Distance(_MovableTransform.position, _TargetNode) < 2.2f)
        {
            if (_TurnOnReach)
            {
                if (Vector3.Distance(_MovableTransform.position, _TargetNode) <= 1f)
                {
                    _TurnOnReach = false;
                    StartCoroutine(DelayTurn());
                    return;
                }
                _MovableTransform.position = Vector3.MoveTowards(_MovableTransform.position, _TargetNode, (_Speed * Time.deltaTime));
                return;
            }

            TargetReachedEvent.Invoke();
        }

        _MovableTransform.position = Vector3.MoveTowards(_MovableTransform.position, _TargetNode, (_Speed * Time.deltaTime));
    }

    #endregion Update

    #region Private Methodds

    private IEnumerator DelayTurn()
    {
        if (_TurnTo == Turn.Right)
        {
            _TurnMagnitude = _MovableTransform.eulerAngles.y + 90;
            leftTarget = _MovableTransform.position + new Vector3(5, 0, 0);
            _PlayerAnim.GetAnimator().Play(AnimConstants.ANIM_TURNRIGHT);
        }
        else
        {
            _TurnMagnitude = 270;
            leftTarget = _MovableTransform.position + new Vector3(-5, 0, 0);
            _PlayerAnim.GetAnimator().Play(AnimConstants.ANIM_TURNLEFT);
        }

        _StartTurning = true;
        yield return new WaitForSeconds(1.5f);
    }

    #endregion Private Methodds
}