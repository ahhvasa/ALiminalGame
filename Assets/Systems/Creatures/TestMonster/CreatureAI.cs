using UnityEngine;

public class CreatureAI : MonoBehaviour
{
    public Creature creature;

    public CreatureVision creatureVision;

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
            Debug.Log("990 Sense = " + sense.ToString());

            VisionSense visionSense = sense as VisionSense;
            if (visionSense == null) { continue; }

            Debug.Log("990 VisionSense = " + visionSense.ToString());

            try
            {
                Player player = visionSense.perceivableObject.GetComponentInParent<Player>();
                if (player == null) { continue; }

                Debug.Log("990 Player = " + player);


                creatureTaskRegister.AddTask(new CreatureTask(100, new CreatureState_Chase(creature, player)));

                Debug.Log("990 Added");
            }
            catch
            {

            }
        }

        creatureTaskRegister.RemoveUnUpdated();
    }

}

