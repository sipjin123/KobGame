﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IView
{
    Transform GetTransform();
    CharacterController GetController();
}