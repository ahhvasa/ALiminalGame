using UnityEngine;

public class ObjectFlipByTarget : MonoBehaviour
{
    [SerializeField] private Transform flipObject;
    [SerializeField] public Vector3 lookTarget;

    private void Update()
    {
        if (lookTarget == null || flipObject == null)
            return;

        Vector3 toTarget = lookTarget.normalized;

        float angle = Vector3.SignedAngle(
            transform.forward,
            toTarget,
            Vector3.up);

        flipObject.transform.localScale =  new Vector3(angle > 0f ? -1 : 1,1,1);
    }
}
