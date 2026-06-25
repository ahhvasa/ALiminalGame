using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class RoomSeer : MonoBehaviour
{
    public List<Room> directlyVisibleRooms = new();

    public List<Room> GetVisibleRooms()
    {
        List<Room> currentDirectlyVisibleRooms = new List<Room>();
        currentDirectlyVisibleRooms.AddRange(directlyVisibleRooms);

        currentDirectlyVisibleRooms.Add(RoomManadger.GetClosestRoom(transform.position));

        List<Room> allVisibleRooms = new List<Room>();

        foreach(var room in currentDirectlyVisibleRooms)
        {
            var visibleRooms = room.GetAllVisibleRooms();
            foreach(var visibleRoom in visibleRooms)
            {
                if (allVisibleRooms.Contains(visibleRoom) == false)
                { allVisibleRooms.Add(visibleRoom); }
            }
        }

        return allVisibleRooms;
    }

    void FixedUpdate()
    {
        List<Room> visibleRooms = GetVisibleRooms();
        List<Room> unVisibleRooms = new List<Room>();

        foreach (Room room in RoomManadger.AllRooms)
        {
            if (visibleRooms.Contains(room) == false)
            {
                unVisibleRooms.Add(room);
            }
        }

        foreach (Room room in unVisibleRooms)
        {
            room.Show(false);
        }
        foreach (Room room in visibleRooms)
        {
            room.Show(true);
        }
    }

    public Room currentRoom;
}
