using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public void Update()
    {
        LookAtDirection(GroundQuad.Point);
    }
    public void LookAtDirection(Vector3 point)
    {
        transform.LookAt(point, Vector3.up);
    }
}