using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    List<PlatformController> _PlatformList;

    public List<Transform> GetPathList(int player)
    {
        List<Transform> nodeList = new List<Transform>();
        foreach(PlatformController platform in _PlatformList)
        {
            foreach(Transform transList in platform.PlatformModel.GetPathNodes(player))
                nodeList.Add(transList);
        }

        return nodeList;
    }
}
