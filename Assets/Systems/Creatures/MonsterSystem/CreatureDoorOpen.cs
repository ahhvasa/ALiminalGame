using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDoorOpen : MonoBehaviour
{
    public Creature creature;
    [SerializeField] private int targetLayer;
    public float doorOpeningTime = 0.5f;

    public float doorBarricadeBreakingPower = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != targetLayer)
            return;

        Open(true, other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != targetLayer)
            return;

        Open(false, other);
    }

    public void Open(bool openOrClose, Collider collider)
    {
        
        RoomDoor roomDoor = collider.GetComponent<RoomDoor>();

        if (roomDoor.IsOpen == false)
        {
            if (openOrClose == true && doorOpeningTime > 0.01f) 
            { 
                if (roomDoor.doorBarricade != null)
                {
                    roomDoor.doorBarricade.StartBreaking();
                    creature.movement.StopForTime(roomDoor.doorBarricade.GetBreakingTime(doorBarricadeBreakingPower), ActionOnBreakDoor);

                    void ActionOnBreakDoor()
                    {
                        roomDoor.doorBarricade.Break();
                        creature.movement.StopForTime(doorOpeningTime, () => roomDoor.Open(openOrClose, transform.position));
                    }

                }
                else
                {
                    creature.movement.StopForTime(doorOpeningTime, () => roomDoor.Open(openOrClose, transform.position));
                }
                
            }
        }
    }
}
