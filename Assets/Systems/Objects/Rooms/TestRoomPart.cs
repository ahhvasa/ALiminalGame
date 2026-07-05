using System.Collections.Generic;
using UnityEngine;

public class TestRoomPart : RoomPart
{
    public bool canSee;

    public override bool CanSeeConnectedRoom(Room hostRoom)
    {
        return canSee;
    }

    public override bool CanWalkToConnectedRoom(Room hostRoom)
    {
        return true;
    }

}