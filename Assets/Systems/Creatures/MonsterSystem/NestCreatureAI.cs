using UnityEngine;

public class NestCreatureAI : CreatureAI
{
    public CreatureDoorOpen creatureDoorOpen;

    public float wanderSpeed_calm = 4;
    public float exploreSpeed_calm = 4;
    public float chaseSpeed_calm = 10;
    public float doorOpenTime_calm = 3;

    public float wanderSpeed_hungry = 4;
    public float exploreSpeed_hungry = 8;
    public float chaseSpeed_hungry = 12;
    public float doorOpenTime_hungry = 0.75f;

    public Room nestRoom;
    public Room[] avaliableNestRooms;

    [SerializeField] private float _hunger;
    public float Hunger
    {
        get { return _hunger; }
        set
        {
            _hunger = value;

            if (_hunger > maximumHunger)
            {
                HungerMode(true);
            }
            else
            {
                HungerMode(false);
            }
        }
    }

    public void HungerMode(bool on)
    {
        if (on)
        {
            soundProvider.enabled = true;
            creatureTaskRegister.RemoveTask(stayInRoomTask);

            creature.movement.wanderSpeed = wanderSpeed_hungry;
            creature.movement.explorationSpeed = exploreSpeed_hungry;
            creature.movement.chaseSpeed = chaseSpeed_hungry;
            creatureDoorOpen.doorOpeningTime = doorOpenTime_hungry;
        }
        else
        {
            soundProvider.enabled = false;
            creatureTaskRegister.AddTask(stayInRoomTask);

            creature.movement.wanderSpeed = wanderSpeed_calm;
            creature.movement.explorationSpeed = exploreSpeed_calm;
            creature.movement.chaseSpeed = chaseSpeed_calm;
            creatureDoorOpen.doorOpeningTime = doorOpenTime_calm;
        }
    }


    public float maximumHunger;
    public float saturationPerItem = 75;

    public CreatureTask stayInRoomTask;
    private Vector3 lastPosition;

    public float minSpeedToLookDown = 1f;
    public Vector3 lookDownAngle;

    public void FixedUpdate()
    {
        Hunger += 1 * Time.deltaTime;

        float velocity = ((transform.position - lastPosition) / Time.deltaTime).magnitude;

        lastPosition = transform.position;

        if (velocity < minSpeedToLookDown)
        {
            creature.transform.rotation = Quaternion.Euler(lookDownAngle);
        }
    }

    new public void Start()
    {
        if (avaliableNestRooms.Length > 0) 
        {
            nestRoom = avaliableNestRooms[Random.Range(0, avaliableNestRooms.Length)];
        }
        

        creatureMemory.TryGetProvider<CreatureVisionSenseProvider>(out visionProvider);
        creatureMemory.TryGetProvider<CreatureSoundSenseProvider>(out soundProvider);
        creatureMemory.TryGetProvider<CreatureSmellSenseProvider>(out smellProvider);

        WanderAround();
        ExploreSound();
        ExploreSmell();
        ChasePlayer();
        BeScared();
        EatFood();

        stayInRoomTask = new CreatureTask(20, new CreatureState_Explore(creature, nestRoom != null ? nestRoom.transform.position : creature.transform.position));
        creatureTaskRegister.AddTask(stayInRoomTask);

        creature.OnEatObject += () => { _hunger -= saturationPerItem; };
        Hunger = 0;

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
                    if (creature.perceivableObject == visibleObject.PerceivableObject) { return false; }

                    if (visibleObject.PerceivableObject.TryGetComponent<ScaryFlag>(out ScaryFlag scaryFlag))
                    {
                        return creature.scaryFlag.scaryMeter < scaryFlag.scaryMeter;
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
                    var state = new CreatureState_Explore(creature, sound.PerceivableObject.transform.position);
                    var task = new CreatureTask(50, state);
                    state.SetParentTask(task);
                    return task;
                },
                onDetect: (Sound sound, CreatureTask task) =>
                {
                    creature.SurpriseSound();
                    (task.state as CreatureState_Explore).lost = false;
                    (task.state as CreatureState_Explore).point = sound.PerceivableObject.transform.position;
                },
                onLost: (Sound sound, CreatureTask task) =>
                {
                    (task.state as CreatureState_Explore).lost = true;
                },
                onFixedUpdate: (Sound sound, CreatureTask task) =>
                {
                    if ((task.state as CreatureState_Explore).lost == true) { return; }

                    if (sound.PerceivableObject == null) { return; }

                    (task.state as CreatureState_Explore).point = sound.PerceivableObject.transform.position;
                },
                removeTaskOnLose: false
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
                    var state = new CreatureState_Explore(creature, objectSmell.PerceivableObject.transform.position);
                    var task = new CreatureTask(40, state);
                    state.SetParentTask(task);
                    return task;
                },
                onDetect: (ObjectSmell objectSmell, CreatureTask task) =>
                {
                    creature.SurpriseSound();
                    (task.state as CreatureState_Explore).lost = false;
                    (task.state as CreatureState_Explore).point = objectSmell.PerceivableObject.transform.position;
                },
                onLost: (ObjectSmell objectSmell, CreatureTask task) =>
                {
                    (task.state as CreatureState_Explore).lost = true;
                },
                onFixedUpdate: (ObjectSmell objectSmell, CreatureTask task) =>
                {
                    if ((task.state as CreatureState_Explore).lost == true) { return; }

                    if (objectSmell.PerceivableObject == null) { return; }

                    (task.state as CreatureState_Explore).point = objectSmell.PerceivableObject.transform.position;
                },
                removeTaskOnLose: false
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




}