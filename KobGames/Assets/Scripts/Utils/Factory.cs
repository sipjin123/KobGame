﻿using System;
using System.Collections.Generic;

public static class Factory
{
    private static readonly Dictionary<Type, object> factoryMap = new Dictionary<Type, object>();

    public static void Register<IT>(IT factory)
    {
        factoryMap[typeof(IT)] = (object)factory;
    }

    public static T Get<T>()
    {
        return (T)factoryMap[typeof(T)];
    }
}