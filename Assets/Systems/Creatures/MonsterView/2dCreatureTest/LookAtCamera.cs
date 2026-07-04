using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        Vector3 direction = CameraManadger.cameraDirection;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}