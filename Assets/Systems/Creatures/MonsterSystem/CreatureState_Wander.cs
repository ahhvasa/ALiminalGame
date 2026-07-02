using UnityEngine;

public class CreatureState_Wander : ICreatureState
{
    public Creature creature;
    public CreatureMovement movement;
    public Vector3[] points;

    public CreatureState_Wander(Creature creature)
    {
        this.creature = creature;
        movement = creature.GetComponent<CreatureMovement>();
        Initialize();
    }

    public void OnEnter()
    {

    }
    public void OnExit()
    {
        movement.Stop();
    }
    public void FixedUpdate()
    {

    }
    public void Update()
    {
        if (!movement.agent.pathPending &&
            movement.agent.remainingDistance <= movement.agent.stoppingDistance)
        {
            if (!movement.agent.hasPath || movement.agent.velocity.sqrMagnitude == 0f)
            {
                SetRandomDestination();
            }
        }
    }


    void Initialize()
    {
        Room[] rooms = MonoBehaviour.FindObjectsOfType<Room>();

        points = new Vector3[rooms.Length];
        for (int i = 0; i < rooms.Length; i++)
        {
            points[i] = rooms[i].roomCenter.transform.position;
        }
    }

    void SetRandomDestination()
    {
        movement.SetDestination(points[UnityEngine.Random.Range(0, points.Length)]);
    }

}


