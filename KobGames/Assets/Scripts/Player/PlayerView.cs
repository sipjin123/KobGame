using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour, IView
{
    [SerializeField]
    private Transform _MovableTransform;

    [SerializeField]
    private ParticleSystem _RunParticles;

    public Transform GetTransform()
    {
        return _MovableTransform;
    }
    [SerializeField]
    private bool _LogTargetReach;

    public BoolEvent RunEvent = new BoolEvent();
    public BoolEvent DeadEvent = new BoolEvent();
    public FloatEvent SpeedEvent = new FloatEvent();
    public Vector3Event TargetPosEvent = new Vector3Event();
    public UnityEvent TargetReachedEvent = new UnityEvent();

    private PlayerViewAnimation _PlayerAnim;
    public void InjectAnim(PlayerViewAnimation anim)
    {
        _PlayerAnim = anim;
    }

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


    bool _StartTurning;
    float _TurnMagnitude = 90;
    bool _TurnOnReach;
    public enum Turn
    {
        Left,
        Right
    }
    public Turn _TurnTo;
    public void TurnOnReach(Turn direction)
    {
        _TurnOnReach = true;
        _TurnTo = direction;
    }

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
            //Debug.LogError("II am receiving target pos event : "+ _);
            _RotDamp = 0;
            _TargetNode = _;
        });
    }

    private void LookAtTarget()
    {
        return;
        if (_MovableTransform)
        {
            _RotDamp += Time.deltaTime * 3;
            var lookPos = _TargetNode - _MovableTransform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            _MovableTransform.rotation = Quaternion.Slerp(_MovableTransform.rotation, rotation, _RotDamp);
        }
    }

    private void FixedUpdate()
    {

        if(_StartTurning )
        {
            
            /*
            _RotDamp += Time.deltaTime * 3;

            Vector3 targetLerp = new Vector3(
                _MovableTransform.eulerAngles.x,
                _TurnMagnitude,
                _MovableTransform.eulerAngles.z);

            if (_LogTargetReach)
                Debug.LogError("TArget Lerp is : " + targetLerp);

            _MovableTransform.eulerAngles = Vector3
                .Lerp(_MovableTransform.eulerAngles, targetLerp, _RotDamp);*/


            _RotDamp += Time.deltaTime * 2;
            

            var lookPos = leftTarget - _MovableTransform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            _MovableTransform.rotation = Quaternion.Slerp(_MovableTransform.rotation, rotation, _RotDamp);

            if(_RotDamp >= 1)
            {
                _StartTurning = false;
            }
        }

        LookAtTarget();
        if (!_RunFlag || _IsDeadFlag || _StartTurning)
        {
            _RunParticles.enableEmission = false;
            return;
        }

        _RunParticles.enableEmission = true;
        if (Vector3.Distance(_MovableTransform.position, _TargetNode) < 2.2f)
        {
            if(_LogTargetReach)
            Debug.LogError("EVENT REACHED");

            if (_TurnOnReach)
            {
                if (Vector3.Distance(_MovableTransform.position, _TargetNode) <= 1)
                {
                    _TurnOnReach = false;
                    Debug.LogError
                        ("INIT TURNING");
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

    IEnumerator DelayTurn()
    {
        if (_TurnTo == Turn.Right)
        {
            _TurnMagnitude = _MovableTransform.eulerAngles.y + 90;
            leftTarget = _MovableTransform.position + new Vector3(5, 0, 0);
        }
        else
        {
            _TurnMagnitude = 270;
            leftTarget = _MovableTransform.position + new Vector3(-5, 0, 0);
        }
        _PlayerAnim.GetAnimator().Play(AnimConstants.ANIM_IDLE);

        _StartTurning = true;
        yield return new WaitForSeconds(1.5f);

    }

    Vector3 leftTarget;
}