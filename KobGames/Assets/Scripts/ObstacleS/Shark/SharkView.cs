using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SharkView : MonoBehaviour
{
    private bool _StartPath;
    [SerializeField]
    private List<Vector3> _NodePath;
    [SerializeField]
    private Vector3 _TargetNode;

    [SerializeField]
    private Transform _MovableTransform;
    public Transform MovableTransform { get { return _MovableTransform; } }

    [SerializeField]
    private float _Speed = 2;

    public UnityEvent TargetReachedEvent = new UnityEvent();
    public Vector3Event TargetPosEvent = new Vector3Event();
    public UnityEvent TriggerObstacle = new UnityEvent();

    bool _StartAtLeft;

    [SerializeField]
    private GameObject _LeftVFXStart, _LefVFXtEnd, _RightVFXStart, _RightVFXEnd;

    float _RotDamper = 0;
    int _PathCounter = 0;

    private void Start()
    {
        TargetPosEvent.AddListener(_ =>
        {
            _TargetNode = _;
        });
    }
    public void UpdatePosition(Vector3 pos)
    {
        _MovableTransform.position = pos;
    }
    public void UpdateRotation(Vector3 rot)
    {
        _MovableTransform.eulerAngles = rot;
    }

    public void EndOfPath()
    {

        _StartPath = false;
    }

    public void InitWarningVFX()
    {
        GameObject obj = _StartAtLeft ? _RightVFXStart : _LeftVFXStart;
        obj.SetActive(true);
    }

    public void TriggerTrap(List<Vector3> path,bool startAtLeft)
    {
        _StartAtLeft = startAtLeft;
        _NodePath = path;
        _TargetNode = _NodePath[0];

        _LeftVFXStart.SetActive(false);
        _LefVFXtEnd.SetActive(false);
        _RightVFXEnd.SetActive(false);
        _RightVFXStart.SetActive(false);
        _PathCounter = 0;
        _StartPath = true;
    }

    private void FixedUpdate()
    {
        if (!_StartPath)
            return;

        if(_TargetNode == null)
        {
            Debug.LogError("I got no target");
            return;
        }

        if (Vector3.Distance(_MovableTransform.position, _TargetNode) < .5f)
        {
            _RotDamper = 0;
            _PathCounter++;
            if(_PathCounter == 4)
            {
                GameObject obj = _StartAtLeft ? _RightVFXEnd : _LefVFXtEnd;
                obj.SetActive(true);
            }
            TargetReachedEvent.Invoke();
        }
        _RotDamper += Time.deltaTime * 15;
        var lookPos = _TargetNode - _MovableTransform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        _MovableTransform.rotation = Quaternion.Slerp(_MovableTransform.rotation, rotation, Time.deltaTime * _RotDamper);
        _MovableTransform.position = Vector3.MoveTowards(_MovableTransform.position, _TargetNode, (_Speed * Time.deltaTime));
    }
    


}
