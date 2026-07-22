using UnityEngine;
using UnityEngine.AI;

public class CreatureMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform currentTarget;

    public float wanderSpeed = 4;
    public float explorationSpeed = 8;
    public float chaseSpeed = 12;

    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public void FollowTarget(Transform target)
    {
        currentTarget = target;
    }
    public void ClearFollowTarget()
    {
        currentTarget = null;
        Stop();
    }
    public void Stop()
    {
        agent.SetDestination(agent.transform.position);
    }

    public void FixedUpdate()
    {
        if (currentTarget == null) { return; }
        agent.SetDestination(currentTarget.position);
    }

    public void SetWanderSpeed()
    {
        agent.speed = wanderSpeed;
    }

    public void SetExplorationSpeed()
    {
        agent.speed = explorationSpeed;
    }

    public void SetChaseSpeed()
    {
        agent.speed = chaseSpeed;
    }
}

public enum moveType
{
    sneak,
    walk,
    fastWalk,
    run
}