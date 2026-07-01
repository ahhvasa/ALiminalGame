using MyLibrary.StateMachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureTaskRegister : MonoBehaviour
{
    public Creature creature;
    [SerializeField][TextArea] private string taskLog;

    public List<CreatureTask> tasks = new();
    
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
        creature.ExecuteTask(GetPriorityTask());
        taskLog = CreatureTask.PrintList(tasks);
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

    public override string ToString()
    {
        return $"Task [{priority}] {state.ToString()}";
    }

    public static string PrintList(IEnumerable<CreatureTask> tasks)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var task in tasks)
        {
            sb.AppendLine(task.ToString());
        }

        return sb.ToString();
    }
}

