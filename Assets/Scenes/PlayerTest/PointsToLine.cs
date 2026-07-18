using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PointsToLine : MonoBehaviour
{
    [SerializeField] private List<Transform> points = new();
    [SerializeField] private LineRenderer lineRenderer;

    private void Reset()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (lineRenderer == null || points == null)
            return;

        lineRenderer.positionCount = points.Count;

        for (int i = 0; i < points.Count; i++)
        {
            if (points[i] != null)
                lineRenderer.SetPosition(i, points[i].position);
        }
    }
}
