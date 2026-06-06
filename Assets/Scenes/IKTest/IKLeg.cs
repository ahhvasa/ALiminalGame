using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class IKLeg : MonoBehaviour
{
    public CustomIK customIK;
    public Transform target;
    public Transform elbow;

    public Vector3 position;

    /// <summary>
    /// In World coordinates
    /// </summary>
    public void SetTarget(Vector3 targetPosition)
    {
        position = targetPosition;
    }
    /// <summary>
    /// In World coordinates
    /// </summary>
    public Vector3 GetTarget()
    {
        return position;
    }

    public void Awake()
    {
        position = target.position;
    }

    public void Update()
    {
        target.position = position;
    }
}
