using UnityEngine;
/// <summary>
/// Один IRoomPart может являться частью сразу 2 комнат.
/// RoomConectedPart это компонент связь между комнатой и этой IRoomPart.
/// Необходим чтобы одна комната могла получить противоположную комнату.
/// </summary>
public class RoomConnectedPart : MonoBehaviour
{
    public Room hostRoom;
    public Room connectedRoom;

    public RoomPart roomPart;

    public bool CanWalkToConnectedRoom()
    {
        return roomPart.CanWalkToConnectedRoom(hostRoom);
    }
    public bool CanSeeConnectedRoom()
    {
        if (roomPart == null) { return false; }
        return roomPart.CanSeeConnectedRoom(hostRoom);
    }
    public bool TryGetConnectedRoom(out Room room)
    {
        return roomPart.TryGetConnectedRoom(hostRoom, out room);
    }



    public void ClaimRoomPart(RoomPart roomPart)
    {
        this.roomPart = roomPart;
        // if (roomPart.TryGetConnectedRoom(hostRoom, out connectedRoom) == false) { Debug.LogError("NO CONNECTED ROOM"); }
    }
}
