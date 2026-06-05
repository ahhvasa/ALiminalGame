using System;
using System.Collections.Generic;
using System.Linq;

public static class ReflectionService
{
    public static IEnumerable<Type> FindAllOfType<T>()
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t =>
        typeof(T).IsAssignableFrom(t)
        && t.IsInterface == false
        && t.IsAbstract == false
        && t.ContainsGenericParameters == false);
    }
}