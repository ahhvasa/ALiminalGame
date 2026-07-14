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
        BeScared();
        EatFood();


        void EatFood()
        {
            new CreatureBehaviourBuilder().Build<VisibleObject, VisionSense>(
                visionProvider,
                this,
                eatObjectTasks,
                validateObject: (VisibleObject visibleObject) =>
                {
                    if (visibleObject.PerceivableObject.TryGetComponent<FoodFlag>(out FoodFlag foodFlag))
                    {
                        return foodFlag.foodType == foodType.meat || foodFlag.foodType == foodType.apple;
                    }
                    return false;
                },
                createAction: (VisibleObject visibleObject) =>
                {
                    var state = new CreatureState_EatObject(creature, visibleObject.gameObject);
                    var task = new CreatureTask(60, state);
                    state.SetParentTask(task);
                    return task;
                },
                onDetect: (VisibleObject visibleObject, CreatureTask task) =>
                {

                },
                onLost: (VisibleObject visibleObject, CreatureTask task) =>
                {

                },
                onFixedUpdate: (VisibleObject visibleObject, CreatureTask task) =>
                {

                }

                );

        }



        void BeScared()
        {
            new CreatureBehaviourBuilder().Build<VisibleObject, VisionSense>(
                visionProvider,
                this,
                runFromObjectTasks,
                validateObject: (VisibleObject visibleObject) =>
                {
                    if (creature.perceivableObject== visibleObject.PerceivableObject) { return false; }

                    if (visibleObject.PerceivableObject.TryGetComponent<ScaryFlag>(out ScaryFlag scaryFlag))
                    {
                        return creature.scaryFlag.scaryMeter <= scaryFlag.scaryMeter;
                    }
                    return false;
                },
                createAction: (VisibleObject visibleObject) =>
                {
                    var state = new CreatureState_RunFrom(creature, visibleObject.transform.position);
                    var task = new CreatureTask(120, state);
                    state.SetParentTask(task);
                    return task;
                },
                onDetect: (VisibleObject visibleObject, CreatureTask task) =>
                {
                    creature.SurpriseSound();
                    (task.state as CreatureState_RunFrom).runFromPoint = visibleObject.perceivableObject.transform.position;
                },
                onLost: (VisibleObject visibleObject, CreatureTask task) =>
                {

                },
                onFixedUpdate: (VisibleObject visibleObject, CreatureTask task) =>
                {
                    (task.state as CreatureState_RunFrom).runFromPoint = visibleObject.perceivableObject.transform.position;
                },
                removeTaskOnLose: false

                );

        }

        void WanderAround()
        {
            creatureTaskRegister.AddTask(new CreatureTask(10, new CreatureState_Wander(creature)));
        }

        void ExploreSound()
        {
            new CreatureBehaviourBuilder().Build<Sound, SoundSense>(
                soundProvider,
                this,
                soundExploreTasks,
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
                    creature.SurpriseSound();
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
                    creature.SurpriseSound();
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
                    creature.SurpriseSound();
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

    public Dictionary<PerceivableObject, CreatureTask> runFromObjectTasks = new();
    public Dictionary<PerceivableObject, CreatureTask> eatObjectTasks = new();

    public Dictionary<PerceivableObject, CreatureTask> playerChaseTasks = new();
    public Dictionary<PerceivableObject, CreatureTask> soundExploreTasks = new();
    public Dictionary<PerceivableObject, CreatureTask> smellExploreTasks = new();

    public void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
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
        Action<TObject, CreatureTask> onFixedUpdate,

        bool removeTaskOnLose = true
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

            creatureAI.OnFixedUpdate -= fixedUpdateAction;

            if (removeTaskOnLose)
            {
                creatureAI.creatureTaskRegister.RemoveTask(task);
            }
        };

    }
}
