using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshAgentLinearMovementOverrite : MonoBehaviour
{
    private float moveSpeed;
    public bool rotateToTarget = true;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = false;
        agent.updateRotation = false;
    }

    public void FixedUpdate()
    {
        moveSpeed = agent.speed;
    }

    public void Update()
    {
        if (agent.pathPending || agent.remainingDistance <= agent.stoppingDistance)
            return;

        Vector3 target = agent.steeringTarget;
        Vector3 direction = target - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.0001f)
        {
            direction.Normalize();

            transform.position += direction * moveSpeed * Time.deltaTime;

            if (rotateToTarget)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        agent.nextPosition = transform.position;
    }
}