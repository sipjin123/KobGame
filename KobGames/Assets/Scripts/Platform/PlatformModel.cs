using System.Collections.Generic;
using UnityEngine;

public class PlatformModel : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _PathNodes_P1, _PathNodes_P2, _PathNodes_P3;

    [SerializeField]
    private List<Transform> _SpawnableNodes;

    public SpawnObstacleEvent SpawnEvent = new SpawnObstacleEvent();

    [SerializeField]
    private List<PlatformSet> _SpawnableNode;

    public List<PlatformSet> SpawnableNode { get { return _SpawnableNode; } }

    public void Initialize()
    {
        int spawnableCount = _SpawnableNodes.Count;
        int index = 0;

        return;
        foreach (var transforms in _SpawnableNodes)
        {
            int nullRandomizer = Random.Range(1, 10);
            int enumCount = ObstacleType.GetNames(typeof(ObstacleType)).Length;
            int obstacleRandomizer = Random.Range(1, enumCount);

            if (nullRandomizer > 2)
            {
                if (index >= spawnableCount - 1 && (ObstacleType)obstacleRandomizer == ObstacleType.Spinner)
                {
                    obstacleRandomizer = (int)ObstacleType.Hammer;
                }
                var spawnSet = new SpawnObstacleSet();
                spawnSet.Pos = transforms.position;
                spawnSet.Rot = transforms.eulerAngles;
                spawnSet.Type = (ObstacleType)obstacleRandomizer;

                SpawnEvent.Invoke(spawnSet);
            }
            index++;
        }
    }

    public List<Transform> GetPathNodes(int player)
    {
        switch (player)
        {
            case 1:
                return _PathNodes_P1;

            case 2:
                return _PathNodes_P2;

            default:
                return _PathNodes_P3;
        }
    }
}