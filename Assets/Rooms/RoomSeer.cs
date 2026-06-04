using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomSeer : MonoBehaviour
{
    List<Room> allRooms;

    void Start()
    {
        allRooms = GameObject.FindObjectsOfType<Room>().ToList();

    }

    void FixedUpdate()
    {
        List<Room> visibleRooms = GetClosestRoom().GetAllVisibleRooms();

        List<Room> unVisibleRooms = new List<Room>();

        foreach (Room room in allRooms)
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

    public Room GetClosestRoom()
    {
        Room closestRoom = null;
        float closestDistance = float.MaxValue;

        foreach (Room room in allRooms)
        {
            float distance = Vector3.Distance(transform.position, room.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRoom = room;
            }
        }

        return closestRoom;
    }

    public Room currentRoom;
}
