using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorRoomPart : RoomPart
{
    public RoomDoor roomDoor;

    public override bool CanSeeConnectedRoom(Room hostRoom)
    {
        return roomDoor.IsOpen ? true : false;
    }
    public override bool CanWalkToConnectedRoom(Room hostRoom)
    {
        return true;
    }
}
