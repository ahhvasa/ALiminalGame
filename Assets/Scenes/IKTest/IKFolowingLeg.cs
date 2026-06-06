using TMPro;
using UnityEngine;

public class IKFolowingLeg : IKLeg
{
    public Transform baseZone;
    public Transform defaultPosition;
    public float maximumDistance;

    public void FixedUpdate()
    {
        if (Vector3.Distance(baseZone.position, target.position) > maximumDistance)
        {
            ReturnLeg();
        }
    }

    void ReturnLeg()
    {
        SetTarget(defaultPosition.position);
    }
}