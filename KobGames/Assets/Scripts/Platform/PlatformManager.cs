using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private List<PlatformController> _PlatformList;

    public PlatformController GetFirstPlatform()
    {
        return _PlatformList[0];
    }

    [SerializeField]
    private List<PlatformController> _PlatformPool;

    [SerializeField]
    private Transform _StartingSpawnPoint;

    [SerializeField]
    private GameLevelData _GameData;

    public SpawnObstacleEvent SpawnEvent = new SpawnObstacleEvent();

    public List<Transform> GetPathList(int player)
    {
        List<Transform> nodeList = new List<Transform>();
        foreach (PlatformController platform in _PlatformList)
        {
            foreach (Transform transList in platform.PlatformModel.GetPathNodes(player))
                nodeList.Add(transList);
        }

        return nodeList;
    }

    public void InjectPlatformRequests(List<PlatformType> platformType)
    {
        bool isInitialized = true;

        int platformCount = platformType.Count;
        for (int i = 0; i < platformCount; i++)
        {
            PlatformController extractedPlatform = _PlatformPool.Find(_ => _.PlatformType == platformType[i]);

            if (isInitialized)
            {
                isInitialized = false;
            }

            extractedPlatform.transform.position = _StartingSpawnPoint.position;
            extractedPlatform.transform.eulerAngles = _StartingSpawnPoint.eulerAngles;

            _PlatformPool.Remove(extractedPlatform);
            _PlatformList.Add(extractedPlatform);
            extractedPlatform.gameObject.SetActive(true);

            if (i >= platformCount - 1)
            {
                SetupListeners();
                return;
            }
            var nextSpawnType = platformType[i + 1];
            var nextExtect = extractedPlatform.PlatformModel.SpawnableNode.Find(_ => _.PlatformType == nextSpawnType);

            var nextSpawnPoint = nextExtect.SpawnPoint;

            _StartingSpawnPoint = nextSpawnPoint;
        }
    }

    private void SetupListeners()
    {
        foreach (var platforms in _PlatformList)
        {
            platforms.PlatformModel.SpawnEvent.AddListener(_ =>
            {
                SpawnEvent.Invoke(_);
            });
            platforms.PlatformModel.Initialize();
        }
    }
}