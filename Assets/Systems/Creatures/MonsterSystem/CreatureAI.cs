using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using Zenject.SpaceFighter;

public class CreatureAI : MonoBehaviour
{
    public Creature creature;

    public CreatureVisionSenseProvider creatureVision;

    public CreatureMemory creatureMemory;

    public CreatureTaskRegister creatureTaskRegister;


    CreatureVisionSenseProvider visionProvider;
    CreatureSoundSenseProvider soundProvider;
    CreatureSmellSenseProvider smellProvider;

    public Action OnFixedUpdate;

    public void Start()
    {
        creatureMemory.TryGetProvider<CreatureVisionSenseProvider>(out visionProvider);
        creatureMemory.TryGetProvider<CreatureSoundSenseProvider>(out soundProvider);
        creatureMemory.TryGetProvider<CreatureSmellSenseProvider>(out smellProvider);

        WanderAround();
        ExploreSound();
        ExploreSmell();
        ChasePlayer();

        void WanderAround()
        {
            creatureTaskRegister.AddTask(new CreatureTask(10, new CreatureState_Wander(creature)));
        }

        void ExploreSound()
        {
            new CreatureBehaviourBuilder().Build<Sound, SoundSense>(
                soundProvider,
                this,
                smellExploreTasks,
                validateObject: (Sound sound) =>
                {
                    return true;
                },
                createAction: (Sound sound) =>
                {
                    return new CreatureTask(50, new CreatureState_Explore(creature, sound.PerceivableObject.transform.position));
                },
                onDetect: (Sound sound, CreatureTask task) =>
                {
                    (task.state as CreatureState_Explore).point = sound.PerceivableObject.transform.position;
                },
                onLost: (Sound sound, CreatureTask task) =>
                {

                },
                onFixedUpdate: (Sound sound, CreatureTask task) =>
                {
                    (task.state as CreatureState_Explore).point = sound.PerceivableObject.transform.position;
                }

                );

        }

        void ExploreSmell()
        {
            new CreatureBehaviourBuilder().Build<ObjectSmell, SmellSense>(
                smellProvider,
                this,
                smellExploreTasks,
                validateObject: (ObjectSmell objectSmell) =>
                {
                    return true;
                },
                createAction: (ObjectSmell objectSmell) =>
                {
                    return new CreatureTask(40, new CreatureState_Explore(creature, objectSmell.PerceivableObject.transform.position));
                },
                onDetect: (ObjectSmell objectSmell, CreatureTask task) =>
                {
                    (task.state as CreatureState_Explore).point = objectSmell.PerceivableObject.transform.position;
                },
                onLost: (ObjectSmell objectSmell, CreatureTask task) =>
                {

                },
                onFixedUpdate: (ObjectSmell objectSmell, CreatureTask task) =>
                {
                    (task.state as CreatureState_Explore).point = objectSmell.PerceivableObject.transform.position;
                }

                );

        }

        void ChasePlayer()
        {
            new CreatureBehaviourBuilder().Build<VisibleObject, VisionSense>(
                visionProvider,
                this,
                playerChaseTasks,
                validateObject: (VisibleObject visibleObject) =>
                {
                    return visibleObject.PerceivableObject.TryGetComponent<Player>(out Player player);
                },
                createAction: (VisibleObject visibleObject) =>
                {
                    return new CreatureTask(100, new CreatureState_Chase(creature));
                },
                onDetect: (VisibleObject visibleObject, CreatureTask task) =>
                {
                    (task.state as CreatureState_Chase).player = visibleObject.perceivableObject.GetComponent<Player>();
                },
                onLost: (VisibleObject visibleObject, CreatureTask task) =>
                {

                },
                onFixedUpdate: (VisibleObject visibleObject, CreatureTask task) =>
                {

                }

                );

        }

    }


    public CreatureTask wanderTask;
    public Dictionary<PerceivableObject, CreatureTask> playerChaseTasks = new();
    public Dictionary<PerceivableObject, CreatureTask> soundExploreTasks = new();
    public Dictionary<PerceivableObject, CreatureTask> smellExploreTasks = new();

    public void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    public void FixedUpdateOld()
    {
        creatureTaskRegister.AddTask(new CreatureTask(10, new CreatureState_Wander(creature)));

        foreach (var sense in creatureMemory.senses)
        {
            if (sense is VisionSense)
            {
                VisionSense visionSense = sense as VisionSense;
                Player player = visionSense.perceivableObject.GetComponentInParent<Player>(); if (player == null) { continue; }
                creatureTaskRegister.AddTask(new CreatureTask(100, new CreatureState_Chase(creature, player)));
            }
            if (sense is SoundSense)
            {
                SoundSense soundSense = sense as SoundSense;
                Player player = soundSense.perceivableObject.GetComponentInParent<Player>(); if (player == null) { continue; }
                creatureTaskRegister.AddTask(new CreatureTask(90, new CreatureState_Chase(creature, player)));
            }
        }

        creatureTaskRegister.RemoveUnUpdated();
    }




    

}

public class CreatureBehaviourBuilder
{
    private Action fixedUpdateAction;

    public void Build<TObject, TSense>(
        CreatureSenseProvider<TObject, TSense> creatureSenseProvider,
        CreatureAI creatureAI,
        Dictionary<PerceivableObject, CreatureTask> taskDictionary,

        Func<TObject, bool> validateObject,
        Func<TObject, CreatureTask> createAction,
        Action<TObject, CreatureTask> onDetect,
        Action<TObject, CreatureTask> onLost,
        Action<TObject, CreatureTask> onFixedUpdate
        )
        where TObject : UnityEngine.Object, IPercivableObject
        where TSense : CreatureSense
    {
        creatureSenseProvider.OnDetectObject += (TObject obj) =>
        {
            if (validateObject(obj) == false) { return; }

            var key = obj.PerceivableObject;

            if (taskDictionary.ContainsKey(key) == false)
            {
                taskDictionary[key] = createAction(obj);
            }
            CreatureTask task = taskDictionary[key];

            onDetect.Invoke(obj, task);
            creatureAI.creatureTaskRegister.AddTask(task);

            fixedUpdateAction = () =>
            {
                onFixedUpdate(obj, task);
            };

            creatureAI.OnFixedUpdate += fixedUpdateAction;
        };

        creatureSenseProvider.OnLoseObject += (TObject obj) =>
        {
            if (validateObject(obj) == false) { return; }

            var key = obj.PerceivableObject;

            var task = taskDictionary[key];

            onLost.Invoke(obj, task);
            creatureAI.creatureTaskRegister.RemoveTask(task);

            creatureAI.OnFixedUpdate -= fixedUpdateAction;
        };

    }
}


