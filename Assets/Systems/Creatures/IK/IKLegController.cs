using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKLegController : MonoBehaviour
{
    public List<IKLegGroup> legGroups;
    public float speed;
    public Vector3 lookTarget;

    public void Start()
    {
        foreach (var group in legGroups)
        {
            foreach (var leg in group.legs)
            {
                leg.t = group.initialT;
            }
        }
    }

    public void FixedUpdate()
    {
        foreach (var group in legGroups)
        {
            foreach (var leg in group.legs)
            {
                leg.speed = speed;
                leg.lineRenderer.transform.rotation = Quaternion.LookRotation(lookTarget);
            }
        }
    }
}

[Serializable]
public class IKLegGroup
{
    public List<LineFollow> legs;
    public float initialT;
}
