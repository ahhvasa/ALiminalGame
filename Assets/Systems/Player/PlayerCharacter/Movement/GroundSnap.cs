using UnityEngine;

public class GroundSnap : MonoBehaviour
{
    public float rayDistance = 1.2f;
    public float positionOnGround = 1;
    public LayerMask groundMask = ~0;

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rayDistance, groundMask))
        {
            Vector3 pos = transform.position;
            pos.y = hit.point.y + positionOnGround;
            transform.position = pos;
        }
    }
}