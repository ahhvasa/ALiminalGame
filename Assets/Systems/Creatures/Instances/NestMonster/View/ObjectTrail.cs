using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrail : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxDistance = 1f;

    public IReadOnlyList<Vector3> Points
    {
        get
        {
            var result = new List<Vector3>(points);
            result.Add(target.position);
            return result;
        }
    }

    private readonly List<Vector3> points = new();

    private void Start()
    {
        if (target == null)
            target = transform;

        points.Add(target.position);
    }

    private void Update()
    {
        Vector3 currentPosition = target.position;

        if (Vector3.Distance(points[points.Count - 1], currentPosition) >= maxDistance)
        {
            points.Add(currentPosition);
        }
    }

    public void Clear()
    {
        points.Clear();
        points.Add(target.position);
    }
}
