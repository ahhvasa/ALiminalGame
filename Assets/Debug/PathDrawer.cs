using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class PathDrawer : MonoBehaviour
{
    public GameObject destinationMarker;
    public NavMeshAgent agent;
    private LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawPath();
    }

    private void DrawPath()
    {
        if (!agent.hasPath)
        {
            line.positionCount = 0;
            return;
        }

        NavMeshPath path = agent.path;
        line.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i] + Vector3.up * 0.05f); // небольшой сдвиг, чтобы линия не мерцала
        }

        destinationMarker.transform.position = agent.destination;
    }
}