using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public static class ZenjectExtention
{
    public static T[] FindAndCreateAllOfType<T>(this DiContainer diContainer)
    {
        return ReflectionService.FindAllOfType<T>()
            .Select(t => (T)Activator.CreateInstance(t))
            .ToArray();
    }
    public static void FindAndBindAllOfType<T>(this DiContainer diContainer)
    {
        IEnumerable<Type> types = ReflectionService.FindAllOfType<T>();
        foreach (var type in types)
        {
            diContainer.BindInterfacesAndSelfTo(type).AsSingle();
        }
    }
}
