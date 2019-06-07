using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformModel : MonoBehaviour
{
    [SerializeField]
    List<Transform> _PathNodes_P1, _PathNodes_P2, _PathNodes_P3;


    public List<Transform> GetPathNodes(int player)
    {
        switch(player)
        {
            case 1:
                return _PathNodes_P1;
                break;
            case 2:
                return _PathNodes_P2;
                break;
            default:
                return _PathNodes_P3;
                break;
        }
    }
}
