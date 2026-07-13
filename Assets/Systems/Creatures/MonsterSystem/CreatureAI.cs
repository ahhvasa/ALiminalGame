using UnityEngine;

public class CreatureAI : MonoBehaviour
{
    public Creature creature;

    public CreatureVisionSense creatureVision;

    public CreatureMemory creatureMemory;

    public CreatureTaskRegister creatureTaskRegister;

    public void Start()
    {
        //creatureTaskRegister.AddTask(new CreatureTask(10, new CreatureState_Wander(creature)));

        //var task1 = new ObjectConditionTask<Player>(creatureVision, creatureTaskRegister, 
            
        //    (Player player) => 
        //    {
        //        var chaseState = new CreatureState_Chase(creature);
        //        chaseState.player = player;

        //        return new CreatureTask(100, chaseState); 
        //    }

        //    );
    }

    public void FixedUpdate()
    {
        //creatureTaskRegister.tasks.Clear();

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


            //VisionSense visionSense = sense as VisionSense;
            //if (visionSense == null) { continue; }

            //try
            //{
            //    Player player = visionSense.perceivableObject.GetComponentInParent<Player>();
            //    if (player == null) { continue; }

            //    Debug.Log("990 Player = " + player);


            //    creatureTaskRegister.AddTask(new CreatureTask(100, new CreatureState_Chase(creature, player)));

            //    Debug.Log("990 Added");
            //}
            //catch
            //{

            //}
        }

        creatureTaskRegister.RemoveUnUpdated();
    }

}

