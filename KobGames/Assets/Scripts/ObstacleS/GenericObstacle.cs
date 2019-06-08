using UnityEngine;

public class GenericObstacle : MonoBehaviour
{
    [SerializeField]
    protected ObstacleType _ObstacleType;

    public ObstacleType GetObstacleType()
    {
        return _ObstacleType;
    }
}