using System;
using System.Collections.Generic;
using UnityEngine;

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
        component = visibleObject.GetComponentInParent<TComponent>();
        if (component == null) { return false; } else { return true; }
    }
}

