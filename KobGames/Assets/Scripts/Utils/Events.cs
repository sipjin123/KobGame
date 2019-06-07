using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events
{

}
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
