using UnityEngine;

public class TrailLineRendererSync : MonoBehaviour
{
    [SerializeField] private ObjectTrail trail;
    [SerializeField] private LineRenderer lr;

    private void Awake()
    {
        if (trail == null)
            trail = FindFirstObjectByType<ObjectTrail>();
    }

    private void FixedUpdate()
    {
        var points = trail.Points;

        lr.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            lr.SetPosition(i, points[i]);
        }
    }
}