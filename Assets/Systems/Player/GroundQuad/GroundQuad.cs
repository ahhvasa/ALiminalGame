using UnityEngine;

public class GroundQuad : MonoBehaviour
{
    public static GroundQuad instance;
    public void Awake()
    {
        instance = this;
    }

    public Vector3 point;

    public static Vector3 Point { get { return instance.point; } }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = LayerMask.GetMask("GroundQuad");

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask, QueryTriggerInteraction.Collide))
        {
            point = hitInfo.point;
        }
    }
}