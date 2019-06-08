using System;
using UnityEngine;

public class ClassCompilation
{
}

[Serializable]
public class PlatformSet
{
    public Transform SpawnPoint;
    public PlatformType PlatformType;
}

public class SpawnObstacleSet
{
    public Vector3 Pos, Rot;
    public ObstacleType Type;
}