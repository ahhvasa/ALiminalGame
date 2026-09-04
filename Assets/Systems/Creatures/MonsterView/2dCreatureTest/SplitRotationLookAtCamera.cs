using UnityEngine;

public class SplitRotationLookAtCamera : MonoBehaviour
{
    [SerializeField] private SplitRotation splitRotation;

    private void Update()
    {
        Vector3 direction = CameraManager.cameraDirection;
        splitRotation.SetDirection(direction);
    }
}
