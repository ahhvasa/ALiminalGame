using MyLibrary.StateMachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public StateMachine<ICreatureState> stateMachine;
    public CreatureTask currentTask;

    public void Awake()
    {
        stateMachine = new StateMachine<ICreatureState>(new CreatureIdleState());
    }

    public void ExecuteTask(CreatureTask task)
    {
        if (task == null) { return; }
        if (currentTask == task) { return; }

        currentTask = task;
        stateMachine.EnterState(task.state);
    }

    public void FixedUpdate()
    {
        stateMachine.Current.FixedUpdate();
    }
    public void Update()
    {
        stateMachine.Current.Update();
    }
}

public abstract class CreatureSense
{
    public PerceivableObject perceivableObject;

    public float lastTimeUpdated;
    public Vector3 position;

    protected CreatureSense(PerceivableObject perceivableObject, float lastTimeUpdated, Vector3 position)
    {
        this.perceivableObject = perceivableObject;
        this.lastTimeUpdated = lastTimeUpdated;
        this.position = position;
    }

    public abstract bool EqualInternal(CreatureSense sense);

    public bool Equal(CreatureSense sense)
    {
        if (sense.perceivableObject == perceivableObject)
        {
            return EqualInternal(sense);
        }
        return false;
    }

    public void Update(CreatureSense sense)
    {
        position = sense.position;
        UpdateInternal(sense);
        lastTimeUpdated = 0;
    }

    public abstract void UpdateInternal(CreatureSense sense);
}

public class VisionSense : CreatureSense
{
    public VisibleObject visibleObject;

    public VisionSense(VisibleObject visibleObject) : base(visibleObject.perceivableObject, 0, visibleObject.perceivableObject.transform.position)
    {
        this.visibleObject = visibleObject;
    }

    public override bool EqualInternal(CreatureSense sense)
    {
        try
        {
            VisionSense visionSense = sense as VisionSense;
            return visibleObject == visionSense.visibleObject;
        }
        catch
        {
            return false;
        }
    }

    public override string ToString()
    {
        return $"[{base.ToString()}] {perceivableObject.gameObject.name} | lastUpdated = {lastTimeUpdated.ToString("F1")} | position = {position}";
    }

    public override void UpdateInternal(CreatureSense sense)
    {

    }
}
