using MyLibrary.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class CreatureAI_2 : MonoBehaviour
{
    public Creature creature;

    public CreatureVision creatureVision;
    public CreatureTaskRegister creatureAITaskSystem;

    public void Start()
    {
        var task1 = new ObjectConditionTask<Player>(creatureVision, creatureAITaskSystem, 
            
            (Player player) => 
            { 
                return new CreatureTask(100, new CreatureState_Chase(creature)); 
            }

            );
    }


}

public class ObjectConditionTask<TComponent>
    where TComponent : Component
{
    private CreatureVision creatureVision;
    private CreatureTaskRegister creatureTaskRegister;
    private Func<TComponent, CreatureTask> createTaskDelegate;
    private Dictionary<TComponent, CreatureTask> tasks = new();

    public ObjectConditionTask(CreatureVision creatureVision, CreatureTaskRegister creatureTaskRegister, Func<TComponent, CreatureTask> createTaskDelegate)
    {
        this.creatureVision = creatureVision;
        this.creatureTaskRegister = creatureTaskRegister;
        this.createTaskDelegate = createTaskDelegate;
        Initialize();
    }

    private void Initialize()
    {
        creatureVision.OnSawObject += 
            (VisibleObject visibleObject) => 
            {
                if (TryGetComponent(visibleObject, out TComponent component) == false) { return; }

                tasks[component] = createTaskDelegate.Invoke(component);
                creatureTaskRegister.AddTask(tasks[component]);
            };

        creatureVision.OnLoseObject +=
            (VisibleObject visibleObject) =>
            {
                if (TryGetComponent(visibleObject, out TComponent component) == false) { return; }

                if (tasks.ContainsKey(component) == false) { return; }

                creatureTaskRegister.RemoveTask(tasks[component]);
                tasks.Remove(component);
            };

    }

    public bool TryGetComponent(VisibleObject visibleObject, out TComponent component)
    {
        component = visibleObject.GetComponentInChildren<TComponent>();
        if (component == null) { return false; } else { return true; }
    }
}



public class CreatureTaskRegister
{
    public Creature creature;

    public List<CreatureTask> tasks;
    public CreatureTask currentTask;

    public CreatureTaskRegister(Creature creature, params CreatureTask[] tasks)
    {
        this.creature = creature;
        this.tasks = tasks.ToList();
    }

    public void AddTask(CreatureTask creatureTask)
    {
        tasks.Add(creatureTask);
    }

    public void RemoveTask(CreatureTask creatureTask)
    {
        tasks.Remove(creatureTask);
    }

    public void FixedUpdate()
    {
        ExecuteTask(GetPriorityTask());
    }

    public CreatureTask GetPriorityTask()
    {
        if (tasks.Count == 0) { return null; }

        CreatureTask currentTask = tasks[0];

        foreach (CreatureTask task in tasks)
        {
            if (currentTask.priority < task.priority)
            {
                currentTask = task;
            }
        }

        return currentTask;
    }

    public void ExecuteTask(CreatureTask task)
    {
        if (currentTask == task) { return; }
        creature.stateMachine.EnterState(task.state);
    }


}

public class CreatureTask
{
    public float priority;
    public ICreatureState state;

    public CreatureTask(float priority, ICreatureState state)
    {
        this.priority = priority;
        this.state = state;
    }
}





public class CreatureIdleState : ICreatureState
{
    public void FixedUpdate()
    {

    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }
}

