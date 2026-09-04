using System.Collections.Generic;
using UnityEngine;

public class ObjectSmellManager : MonoBehaviour
{
    public static ObjectSmellManager Instance;
    public List<ObjectSmell> objectSmell = new();
    public void Awake()
    {
        Instance = this;
    }

}
