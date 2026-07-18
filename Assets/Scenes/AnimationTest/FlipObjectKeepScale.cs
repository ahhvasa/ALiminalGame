using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipObjectKeepScale : MonoBehaviour
{
    public Transform host;
    Vector3 initialScale;
    void Start()
    {
        initialScale = transform.localScale;
    }
    private void Update()
    {
        if (host.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(initialScale.x  * - 1, initialScale.y, initialScale.z);
        }
        else
        {
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        }
    }
}
