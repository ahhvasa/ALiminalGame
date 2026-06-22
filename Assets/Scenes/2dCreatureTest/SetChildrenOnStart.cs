using System.Collections.Generic;
using UnityEngine;

public class SetChildrenOnStart : MonoBehaviour
{
    public List<Transform> children;
    public void Awake()
    {
        foreach (Transform t in children)
        {
            t.SetParent(transform);
        }
    }
}