using UnityEngine;
using UnityEngine.AI;

public class CreatureMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    Transform currentTarget;
    public void FollowTarget(Transform target)
    {
        currentTarget = target;
    }
    public void ClearFollowTarget()
    {
        currentTarget = null;
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