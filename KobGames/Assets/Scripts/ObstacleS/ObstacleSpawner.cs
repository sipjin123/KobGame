using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _ObstacleList;

    [SerializeField]
    private List<GameObject> _SpawnedList = new List<GameObject>();

    [SerializeField]
    private GameObject _PendulumTrap, _SharkTrap, _SpinnerTrap, _CannonTrap;

    [SerializeField]
    private Transform _Pool;

    public void RequestObstacleAt(SpawnObstacleSet spawnSet)
    {
        var obstacle = _ObstacleList.Find(_ => _.GetComponent<GenericObstacle>().GetObstacleType() == spawnSet.Type);

        if (obstacle == null)
        {
            switch (spawnSet.Type)
            {
                case ObstacleType.Cannon:
                    obstacle = Instantiate(_CannonTrap, _Pool);
                    break;

                case ObstacleType.Sharks:
                    obstacle = Instantiate(_SharkTrap, _Pool);
                    break;

                case ObstacleType.Hammer:
                    obstacle = Instantiate(_PendulumTrap, _Pool);
                    break;

                case ObstacleType.Spinner:
                    obstacle = Instantiate(_SpinnerTrap, _Pool);
                    break;
            }
            _ObstacleList.Add(obstacle);
        }

        _ObstacleList.RemoveAt(0);
        obstacle.transform.position = spawnSet.Pos;
        obstacle.transform.eulerAngles = spawnSet.Rot;

        obstacle.SetActive(true);
        _SpawnedList.Add(obstacle);
    }
}