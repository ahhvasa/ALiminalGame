using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPoint : MonoBehaviour
{
    public static CameraPoint Instance;
    public void Awake()
    {
        Instance = this;
    }

    public FollowPosition followPosition;

    public void FollowPlayer()
    {
        followPosition.enabled = true;
        followPosition.target = GameObject.FindFirstObjectByType<Player>().transform;
    }

    public void FollowObject(Transform target)
    {
        followPosition.enabled = true;
        followPosition.target = target;
    }

    public void SetPosition(Vector3 position)
    {
        followPosition.enabled = false;
        transform.position = position;
    }
}
