using System.Linq;
using UnityEngine;

public class CreatureState_Wander : ICreatureState
{
    public Creature creature;
    public CreatureMovement movement;
    public Room[] rooms;

    public CreatureState_Wander(Creature creature)
    {
        this.creature = creature;
        movement = creature.movement;
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
        SceneSearchService.TryFindNearest<Room>(creature.transform.position, 10, out Room room);
        rooms = room.GetAllConnectedRooms().ToArray();
    }

    void SetRandomDestination()
    {
        movement.SetDestination(rooms[UnityEngine.Random.Range(0, rooms.Length)].roomCenter.transform.position);
    }

}



public class CreatureState_Explore : ICreatureState
{
    public Creature creature;
    public CreatureMovement movement;
    public Vector3 point;

    public CreatureState_Explore(Creature creature, Vector3 point)
    {
        this.creature = creature;
        movement = creature.movement;
        this.point = point;
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
                SetDestination();
            }
        }
    }

    void SetDestination()
    {
        movement.SetDestination(point);
    }

}


public class CreatureState_EatObject : ICreatureState
{
    public Creature creature;
    public CreatureMovement movement;
    public GameObject target;
    public CreatureTask creatureTask;

    public CreatureState_EatObject(Creature creature, GameObject target)
    {
        this.creature = creature;
        movement = creature.movement;
        this.target = target;
    }

    public void SetParentTask(CreatureTask creatureTask)
    {
        this.creatureTask = creatureTask;
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
        if (target.activeSelf == false)
        {
            creature.taskRegister.RemoveTask(creatureTask);
            return;
        }

        SetDestination();

        if (Vector3.Distance(creature.transform.position, target.transform.position) < 1)
        {
            EatObject();
        }

    }
    public void Update()
    {

    }

    void SetDestination()
    {
        movement.SetDestination(target.transform.position);
    }

    void EatObject()
    {
        creature.creatureViewAnimation?.EatObject();
        creature.taskRegister.RemoveTask(creatureTask);
        target.gameObject.SetActive(false);
    }
}




public class CreatureState_RunFrom : ICreatureState
{
    public Creature creature;
    public CreatureMovement movement;
    public Room[] rooms;
    public Vector3 runFromPoint;
    public CreatureTask creatureTask;

    public CreatureState_RunFrom(Creature creature, Vector3 runFromPoint)
    {
        this.creature = creature;
        movement = creature.movement;
        this.runFromPoint = runFromPoint;
        Initialize();
    }

    public void SetParentTask(CreatureTask creatureTask)
    {
        this.creatureTask = creatureTask;
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
        SetDestination();

        if (!movement.agent.pathPending &&
            movement.agent.remainingDistance <= movement.agent.stoppingDistance)
        {
            if (!movement.agent.hasPath || movement.agent.velocity.sqrMagnitude == 0f)
            {
                creature.taskRegister.RemoveTask(creatureTask);
            }
        }
    }
    public void Update()
    {
    }


    void Initialize()
    {
        SceneSearchService.TryFindNearest<Room>(creature.transform.position, 10, out Room room);
        rooms = room.GetAllConnectedRooms().ToArray();
    }

    void SetDestination()
    {
        Vector3 targetPosition = creature.transform.position - (runFromPoint - creature.transform.position).normalized * 10;

        SceneSearchService.TryFindNearest<Room>(runFromPoint, 10, out Room runFromRoom);

        movement.SetDestination(rooms
            .OrderBy(room => 

            (Vector3.Distance(room.roomCenter.transform.position, targetPosition)) + 
            (runFromRoom.GetAllVisibleRooms().Contains(room) ? 50 : 0)
            
            )
            .FirstOrDefault().roomCenter.transform.position);
    }

}
