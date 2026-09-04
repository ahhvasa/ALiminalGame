using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        Vector3 direction = CameraManager.cameraDirection;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}