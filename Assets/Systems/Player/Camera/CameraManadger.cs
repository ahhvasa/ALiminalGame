using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraManadger : MonoBehaviour
{
    public static CameraManadger Instance;

    public CameraPost[] cameraPosts;

    public void Awake()
    {
        Instance = this;
        cameraPosts = FindObjectsOfType<CameraPost>();
        UpdateCameras(false);
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

    public void UpdateCameras(bool showOrHide)
    {
        foreach (var post in cameraPosts)
        {
            if (post.currentItem == null)
            {
                post.interactableObjectFlag.active = showOrHide;
            }
            else
            {
                post.interactableObjectFlag.active = true;
            }
        }
    }
}
