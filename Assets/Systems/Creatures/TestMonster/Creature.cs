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
