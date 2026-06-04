using System.Collections.Generic;
using UnityEngine;

public class RoomDoorWay : MonoBehaviour
{
    private void Start()
    {
        door = GetOrCreateDoor(transform.position);
        door.doorWays.Add(this);
    }


    [SerializeField] private RoomDoor roomDoorPrefab;

    public RoomDoor GetOrCreateDoor(Vector3 position, float maximumDistance = 1)
    {
        if (SceneSearchService.TryFindNearest<RoomDoor>(position, maximumDistance, out RoomDoor door))
        {
            return door;
        }
        else
        {
            return CreateDoor();
        }

        RoomDoor CreateDoor()
        {
            RoomDoor createdDoor = createdDoor = Instantiate(roomDoorPrefab, position, Quaternion.identity);
            createdDoor.transform.LookAt(room.transform);
            return createdDoor;
        }
    }


    public Room GetConnectedRoom()
    {
        List<Room> rooms = door.GetRooms();
        foreach (Room room in rooms)
        {
            if (room != this.room) { return room; }
        }
        return room;
        throw new System.Exception("No connected room");
    }

    public bool IsOpen()
    {
        return door.IsOpen;
    }

    public Room room;
    public RoomDoor door;
}
