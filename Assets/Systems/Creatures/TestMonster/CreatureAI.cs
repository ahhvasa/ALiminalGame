using UnityEngine;

public class CreatureAI : MonoBehaviour
{
    public Creature creature;

    public CreatureVision creatureVision;
    public CreatureTaskRegister creatureTaskRegister;

    public void Start()
    {
        creatureTaskRegister.AddTask(new CreatureTask(10, new CreatureState_Wander(creature)));

        var task1 = new ObjectConditionTask<Player>(creatureVision, creatureTaskRegister, 
            
            (Player player) => 
            {
                var chaseState = new CreatureState_Chase(creature);
                chaseState.player = player;

                return new CreatureTask(100, chaseState); 
            }

            );
    }


}

