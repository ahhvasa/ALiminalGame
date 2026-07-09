using System.Collections.Generic;
using UnityEngine;

public class WallRoomPart : RoomPart
{
    public override bool CanSeeConnectedRoom(Room hostRoom)
    {
        return false;
    }
    public override bool CanWalkToConnectedRoom(Room hostRoom)
    {
        return false;
    }
}