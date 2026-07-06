public class EmptyRoomPart : RoomPart
{
    public override bool CanSeeConnectedRoom(Room hostRoom)
    {
        return true;
    }
    public override bool CanWalkToConnectedRoom(Room hostRoom)
    {
        return true;
    }
}
