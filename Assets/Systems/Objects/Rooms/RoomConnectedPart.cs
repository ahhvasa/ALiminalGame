using UnityEngine;

public class RoomConnectedPart : MonoBehaviour
{
    public Room hostRoom;
    public RoomPart roomPart;

    public bool CanWalkToConnectedRoom()
    {
        if (roomPart == null) { return false; }
        return roomPart.CanWalkToConnectedRoom(hostRoom);
    }
    public bool CanSeeConnectedRoom()
    {
        if (roomPart == null) { return false; }
        return roomPart.CanSeeConnectedRoom(hostRoom);
    }
    public bool TryGetConnectedRoom(out Room room)
    {
        if (roomPart == null) { room = null; return false; }
        return roomPart.TryGetConnectedRoom(hostRoom, out room);
    }
}
