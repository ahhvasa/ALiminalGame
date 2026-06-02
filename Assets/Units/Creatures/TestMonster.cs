using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMonster : MonoBehaviour
{
    public Vector3[] points;
    public NavMeshAgent agent;

    void Start()
    {
        Room[] rooms = FindObjectsOfType<Room>();

        points = new Vector3[rooms.Length];
        for (int i = 0; i < rooms.Length; i++)
        {
            points[i] = rooms[i].roomCenter.transform.position;
        }
    }

    void Update()
    {
        if (!agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                SetRandomDestination();
            }
        }
    }

    void SetRandomDestination()
    {
        Debug.Log("SetRandomDestination");
        agent.SetDestination(points[Random.Range(0, points.Length)]);
    }
}
