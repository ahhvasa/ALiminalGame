using System.Collections.Generic;
using UnityEngine;

public class ObjectSmellManadger : MonoBehaviour
{
    public static ObjectSmellManadger Instance;
    public List<ObjectSmell> objectSmell = new();
    public void Awake()
    {
        Instance = this;
    }

}
