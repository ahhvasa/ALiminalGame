using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LineExtention
{

    public static Vector3 GetPoint(LineRenderer lineRenderer, float t)
    {
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);


        if (lineRenderer.loop == true)
        {
            List<Vector3> pointsList = points.ToList<Vector3>();
            pointsList.Add(lineRenderer.GetPosition(0));
            points = pointsList.ToArray();
        }

        if (!lineRenderer.useWorldSpace)
        {
            Transform tr = lineRenderer.transform;

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = tr.TransformPoint(points[i]);
            }
        }

        return GetPoint(points, t);
    }

    public static Vector3 GetPoint(IReadOnlyList<Vector3> points, float t)
    {
        if (points == null || points.Count == 0)
            return Vector3.zero;

        if (points.Count == 1)
            return points[0];

        t = Mathf.Clamp01(t);

        float totalLength = 0f;

        for (int i = 0; i < points.Count - 1; i++)
        {
            totalLength += Vector3.Distance(points[i], points[i + 1]);
        }

        if (totalLength <= Mathf.Epsilon)
            return points[0];

        float targetDistance = totalLength * t;
        float currentDistance = 0f;

        for (int i = 0; i < points.Count - 1; i++)
        {
            float segmentLength = Vector3.Distance(points[i], points[i + 1]);

            if (currentDistance + segmentLength >= targetDistance)
            {
                float localT = (targetDistance - currentDistance) / segmentLength;

                return Vector3.Lerp(
                    points[i],
                    points[i + 1],
                    localT);
            }

            currentDistance += segmentLength;
        }

        return points[points.Count - 1];
    }
}