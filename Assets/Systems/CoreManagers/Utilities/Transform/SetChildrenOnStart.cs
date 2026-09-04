using System.Collections.Generic;
using UnityEngine;

public class SetChildrenOnStart : MonoBehaviour
{
    public bool setZeroPosition;

    public List<Transform> children;
    public void Awake()
    {
        foreach (Transform t in children)
        {
            t.SetParent(transform);
            if (setZeroPosition) { t.transform.localPosition = Vector3.zero; }
        }
    }
}