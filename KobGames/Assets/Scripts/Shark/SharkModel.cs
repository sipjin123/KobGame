using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SharkModel : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _LeftPathList, _RightPathList;

    [SerializeField]
    private List<Vector3> _CurrentPathList;
    [SerializeField]
    private Transform _StartNode;

    public Vector3Event StartPostUpdate = new Vector3Event();

    bool _StartAtLeft = true;

    [SerializeField]
    private Transform _TransformObject;

    float _manualTimer;

    [SerializeField]
    float _Cooldown = 5;

    bool _Started = false;
    public UnityEvent TriggerObstacle = new UnityEvent();

    private void Start()
    {
        StartPostUpdate.Invoke(_StartNode.position);
    }

    public void Return()
    {
        _Started = false;
        _manualTimer = 0;

        _StartAtLeft = !_StartAtLeft;
        if (_StartAtLeft)
        {
            
        }

    }

    private void FixedUpdate()
    {
        if (_Started)
            return;

        if(_manualTimer < _Cooldown)
        {
            _manualTimer += Time.deltaTime;
        }
        else
        {
            _Started = true;
            TriggerObstacle.Invoke();
        }
    }
    public Vector3 GetNextNode()
    {
        Vector3 vecNode = new Vector3();
        if (_CurrentPathList.Count > 0)
        {
            _CurrentPathList.RemoveAt(0);
            vecNode = _CurrentPathList.Count > 0 ? _CurrentPathList[0] : Vector3.zero;
        }
        else
            return Vector3.zero;
        return vecNode;
    }

    public List<Vector3> GetPathList()
    {
        List<Vector3> vecList = new List<Vector3>();

        if (_StartAtLeft)
        {
            foreach (Transform pos in _LeftPathList)
                vecList.Add(pos.position);
        }
        else
        {
            foreach (Transform pos in _RightPathList)
                vecList.Add(pos.position);
        }
        _CurrentPathList = vecList;
        return vecList;
    }
}
