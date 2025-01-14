﻿using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameStateEvent : UnityEvent<GameStates>
{
}

public class BoolEvent : UnityEvent<bool>
{
}

public class FloatEvent : UnityEvent<float>
{
}

public class Vector3Event : UnityEvent<Vector3>
{
}

public class CharStateEvent : UnityEvent<CharacterStates>
{
}

public class ObjectEvent : UnityEvent<GameObject>
{
}

public class SpawnObstacleEvent : UnityEvent<SpawnObstacleSet>
{
}