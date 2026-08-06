using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

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

    public bool canChangeSpeed = true;
    public float savedSpeed;

    public void SetWanderSpeed()
    {
        if (canChangeSpeed == false) { savedSpeed = wanderSpeed; return; }
        agent.speed = wanderSpeed;
    }

    public void SetExplorationSpeed()
    {
        if (canChangeSpeed == false) { savedSpeed = explorationSpeed; return; }
        agent.speed = explorationSpeed;
    }

    public void SetChaseSpeed()
    {
        if (canChangeSpeed == false) { savedSpeed = chaseSpeed; return; }
        agent.speed = chaseSpeed;
    }

    public void SetStopSpeed()
    {
        agent.speed = 0f;
    }

    public void StopForTime(float time, Action action)
    {
        if (stopCoroutine != null)
        {
            StopCoroutine(stopCoroutine);
            stopCoroutine = null;
        }
        stopCoroutine = StartCoroutine(StopForTimeCoroutine(time, action));
    }
    Coroutine stopCoroutine;

    private IEnumerator StopForTimeCoroutine(float time, Action action)
    {
        canChangeSpeed = false;
        savedSpeed = agent.speed;
        SetStopSpeed();
        yield return new WaitForSeconds(time);
        agent.speed = savedSpeed;
        canChangeSpeed = true;
        action?.Invoke();
    }

}

public enum moveType
{
    sneak,
    walk,
    fastWalk,
    run
}