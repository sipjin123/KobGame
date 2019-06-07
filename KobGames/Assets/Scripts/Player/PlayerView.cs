using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    bool _RunFlag;
    [SerializeField]
    bool _IsDeadFlag;

    [SerializeField]
    float _Speed;

    void Start()
    {
        _SpeedEvent.AddListener(_ => _Speed = _);
        _RunEvent.AddListener(_ => _RunFlag = _);
        _DeadEvent.AddListener(_ => _IsDeadFlag = _);
    }

    void Update()
    {
        if (!_RunFlag)
            return;

        _CharController.Move(transform.forward * (_Speed * Time.deltaTime));
    }
}
