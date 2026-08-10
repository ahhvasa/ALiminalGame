using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantPosition : MonoBehaviour
{
    public float yPosition;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
    }
}
