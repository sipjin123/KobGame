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

    [SerializeField]
    private bool _RunFlag;

    [SerializeField]
    private bool _IsDeadFlag;

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
        LookAtTarget();
        if (!_RunFlag || _IsDeadFlag)
        {
            _RunParticles.enableEmission = false;
            return;
        }

        _RunParticles.enableEmission = true;
        if (Vector3.Distance(_MovableTransform.position, _TargetNode) < 2.2f)
        {
            if(_LogTargetReach)
            Debug.LogError("EVENT REACHED");
            TargetReachedEvent.Invoke();
        }

        _MovableTransform.position = Vector3.MoveTowards(_MovableTransform.position, _TargetNode, (_Speed * Time.deltaTime));
    }
}