using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform roomCenter;
    public List<RoomDoorWay> doorWays;
    public RoomZone roomZone;

    /// <summary>
    /// All adjacent rooms.
    /// </summary>
    public List<Room> GetAdjacentRooms()
    {
        List<Room> rooms = new List<Room>();
        rooms.Add(this);
        foreach (RoomDoorWay doorWay in doorWays)
        {
            rooms.Add(doorWay.GetConnectedRoom());
        }
        return rooms;
    }
    /// <summary>
    /// Adjacent visible rooms.
    /// </summary>
    public List<Room> GetAdjacentVisibleRooms()
    {
        List<Room> rooms = new List<Room>();
        rooms.Add(this);
        foreach (RoomDoorWay doorWay in doorWays)
        {
            if (doorWay.IsOpen())
            { rooms.Add(doorWay.GetConnectedRoom()); }
        }
        return rooms;
    }

    /// <summary>
    /// All visible rooms from this room.
    /// </summary>
    public List<Room> GetAllVisibleRooms()
    {
        List<Room> allRooms = GetAdjacentVisibleRooms();

        for (int i = 0; i != allRooms.Count; i++)
        {
            TryAddRooms(allRooms[i].GetAdjacentVisibleRooms());
        }

        void TryAddRooms(List<Room> rooms)
        {
            foreach (Room room in rooms)
            {
                if (allRooms.Contains(room) == false)
                {
                    allRooms.Add(room);
                }
            }

        }

        return allRooms;
    }


    public void Show(bool show)
    {
        GetComponent<VisibleObject>().Show(show);
        roomZone.Show(show);
    }



    public List<VisibleObject> GetAllVisibleObjects()
    {
        return roomZone.visibleObjects;
    }
}
