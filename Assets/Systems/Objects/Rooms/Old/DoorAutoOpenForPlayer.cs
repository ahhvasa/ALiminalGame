public class DoorAutoOpenForPlayer : OnPlayerNear
{
    public RoomDoor roomDoor;
    public override void Activate(bool playerClose)
    {
        roomDoor.Open(playerClose);
    }
}
