using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraManadger : MonoBehaviour
{
    public static CameraManadger Instance;

    public void Awake()
    {
        Instance = this;
    }

    public Camera camera;
    public Transform point;

    public static Vector3 cameraDirection
    {
        get
        {
            return (Instance.point.position - Instance.camera.transform.position).normalized;
        }
    }
}
