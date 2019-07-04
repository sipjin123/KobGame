using System;
using UnityEngine;

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

[Serializable]
public class SFXCombination
{
    public SFX SFX;
    public AudioSource AudioSource;
}