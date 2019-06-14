using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour, IModel
{
    [SerializeField]
    private CharacterStates _CharacterStates;

    [SerializeField]
    private CharacterData _CharData;

    public CharacterData GetCharData()
    {
        return _CharData;
    }

    [SerializeField]
    private Transform _CurrentNode;

    public Transform CurrentNode { get { return _CurrentNode; } }

    [SerializeField]
    private List<Transform> _NodePaths;

    [SerializeField]
    private bool _LogNode;

    public void InjectNodes(List<Transform> nodes)
    {
        _NodePaths = nodes;
        _CurrentNode = _NodePaths[0];
    }

    public Transform GetNextNode()
    {
        if (_NodePaths.Count > 0)
        {
            if(_LogNode)
            Debug.LogError("Removing");
            _NodePaths.RemoveAt(0);
            if(_LogNode)
            Debug.LogError("My next node is : "+_CurrentNode.name);
            _CurrentNode = _NodePaths.Count > 0 ? _NodePaths[0] : null;
            return _CurrentNode;
        }
        else
            return null;
    }

    public CharStateEvent CharStateEvent = new CharStateEvent();

    public void UpdateState(CharacterStates state)
    {
        _CharacterStates = state;
        CharStateEvent.Invoke(state);
    }

    public float GetSpeed()
    {
        return _CharData.Speed;
        switch (_CharacterStates)
        {
            case CharacterStates.Running:
                return _CharData.Speed;

            default:
                return 0;
        }
    }
}