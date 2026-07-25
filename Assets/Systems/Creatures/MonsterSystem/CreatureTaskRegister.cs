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
    public List<CreatureTask> updatedTasks = new();

    public void AddTask(CreatureTask creatureTask)
    {
        foreach (var task in tasks)
        {
            if (task.Equal(creatureTask))
            {
                updatedTasks.Add(task);
                task.Update(creatureTask);
                return;
            }
        }

        updatedTasks.Add(creatureTask);
        tasks.Add(creatureTask);
    }

    public void RemoveTask(CreatureTask creatureTask)
    {
        tasks.Remove(creatureTask);
    }

    public void RemoveUnUpdated()
    {
        for (int i = 0; i != tasks.Count; i++)
        {
            var task = tasks[i];
            if (updatedTasks.Contains(task) == false)
            {
                tasks.Remove(task);
                i--;
            }
        }

        updatedTasks.Clear();
    }

    public void FixedUpdate()
    {
        creature.ExecuteTask(GetPriorityTask());

        #if UNITY_EDITOR
        taskLog = CreatureTask.PrintList(tasks);
        #endif

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

    public bool Equal(CreatureTask task)
    {
        return this == task;

        //if (priority != task.priority) { return false; }
        //if (state.GetType() != task.state.GetType()) { return false; }
        //return true;
    }

    public void Update(CreatureTask task)
    {
        state = task.state;
    }
}

