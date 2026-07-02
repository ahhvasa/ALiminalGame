using UnityEngine;
using UnityEngine.AI;

public class CreatureMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform currentTarget;

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
}

public enum moveType
{
    sneak,
    walk,
    fastWalk,
    run
}