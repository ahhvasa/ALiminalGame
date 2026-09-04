using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListDebugExtention
{
    public static string ListToText<T>(this List<T> list)
    {
        return string.Join("\n", list.Select(s => s.ToString()));
    }
}
