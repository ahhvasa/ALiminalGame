using UnityEngine;

public class SplitRotationTest : MonoBehaviour
{
    [SerializeField] private SplitRotation splitRotation;
    [SerializeField] private Transform lookTarget;

    private void Update()
    {
        if (splitRotation == null || lookTarget == null)
            return;

        Vector3 direction = lookTarget.position - transform.position;
        splitRotation.SetDirection(direction);
    }
}