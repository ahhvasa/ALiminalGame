using UnityEngine;

public class SplitRotation : MonoBehaviour
{
    [SerializeField] private Transform yPart;
    [SerializeField] private Transform xPart;

    public void SetDirection(Quaternion targetRotation)
    {
        Vector3 euler = targetRotation.eulerAngles;

        yPart.localRotation = Quaternion.Euler(0f, euler.y, 0f);
        xPart.localRotation = Quaternion.Euler(euler.x, 0f, 0f);
    }

    public void SetDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.0001f)
            return;

        SetDirection(Quaternion.LookRotation(direction.normalized));
    }
}
