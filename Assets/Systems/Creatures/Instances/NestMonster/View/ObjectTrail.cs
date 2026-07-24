using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrail : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float maxDistance = 1f;
    [SerializeField] private int maximumPoints = 20;

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

    public float timer = 0;
    public float maxTimer = 1;

    private void Update()
    {
        Vector3 currentPosition = target.position;

        if (Vector3.Distance(points[points.Count - 1], currentPosition) >= maxDistance)
        {
            AddPoint(currentPosition);
        }

        timer += Time.deltaTime;
        if (timer > maxTimer)
        {
            AddPoint(currentPosition);
        }
    }

    public void AddPoint(Vector3 point)
    {
        timer = 0;
        points.Add(point);

        while (points.Count >= maximumPoints)
        {
            points.RemoveAt(0);
        }
    }

    public void Clear()
    {
        points.Clear();
        points.Add(target.position);
    }
}
