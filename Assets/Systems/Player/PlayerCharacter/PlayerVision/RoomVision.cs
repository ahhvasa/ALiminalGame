using System.Collections.Generic;
using UnityEngine;

public abstract class RoomVision : MonoBehaviour
{
    public List<Room> directlyVisibleRooms = new();
    public List<Room> GetVisibleRooms()
    {
        List<Room> currentDirectlyVisibleRooms = new List<Room>();
        currentDirectlyVisibleRooms.AddRange(directlyVisibleRooms);

        currentDirectlyVisibleRooms.Add(RoomManadger.GetClosestRoom(transform.position));

        List<Room> allVisibleRooms = new List<Room>();

        foreach (var room in currentDirectlyVisibleRooms)
        {
            var visibleRooms = room.GetAllVisibleRooms();
            foreach (var visibleRoom in visibleRooms)
            {
                if (allVisibleRooms.Contains(visibleRoom) == false)
                { allVisibleRooms.Add(visibleRoom); }
            }
        }

        return allVisibleRooms;
    }
}
