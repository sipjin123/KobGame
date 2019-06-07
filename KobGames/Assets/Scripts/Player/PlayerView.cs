using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour, IView
{
    [SerializeField]
    private Transform _MovableTransform;

    public Transform GetTransform()
    {
        return _MovableTransform;
    }

    [SerializeField]
    private CharacterController _CharController;

    public CharacterController GetController()
    {
        return _CharController;
    }


    public BoolEvent _RunEvent = new BoolEvent();
    public BoolEvent _DeadEvent = new BoolEvent();
    public FloatEvent _SpeedEvent = new FloatEvent();
    public Vector3Event _TargetPosEvent = new Vector3Event();
    public UnityEvent _TargetReachedEvent = new UnityEvent();

    [SerializeField]
    bool _RunFlag;
    [SerializeField]
    bool _IsDeadFlag;

    [SerializeField]
    Vector3 _TargetNode;

    [SerializeField]
    float _Speed;

    void Start()
    {
        _SpeedEvent.AddListener(_ => _Speed = _);
        _RunEvent.AddListener(_ => _RunFlag = _);
        _DeadEvent.AddListener(_ => _IsDeadFlag = _);
        _TargetPosEvent.AddListener(_ => 
        {
            _TargetNode = _;
        });
    }

    void FixedUpdate()
    {
        if (!_RunFlag)
            return;

            if (Vector3.Distance(_MovableTransform.position, _TargetNode) < 2)
            {
                _TargetReachedEvent.Invoke();
            }
            Debug.LogError("Going to position at " + _Speed);
            _MovableTransform.position = Vector3.MoveTowards(_MovableTransform.position, _TargetNode, (_Speed * Time.deltaTime));
            //_CharController.Move(transform.forward * (_Speed * Time.deltaTime));
    }
}
