using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UIElements;


public class PlayerRoomVision : RoomVision
{
    public bool seeEveryRoom;

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

        List<VisibleObject> visibleObjects = new List<VisibleObject>();
        List<VisibleObject> unVisibleObjects = FindObjectsOfType<VisibleObject>().ToList();

        foreach (Room room in unVisibleRooms)
        {
            if (seeEveryRoom) { visibleObjects.AddRange(room.GetAllVisibleObjects()); continue; }

            // unVisibleObjects.AddRange(room.GetAllVisibleObjects());
        }
        foreach (Room room in visibleRooms)
        {
            visibleObjects.AddRange(room.GetAllVisibleObjects());
        }



        foreach (VisibleObject visibleObject in unVisibleObjects)
        {
            if (visibleObjects.Contains(visibleObject)) { continue; }

            visibleObject.Show(false);
        }

        foreach (VisibleObject visibleObject in visibleObjects)
        {
            visibleObject.Show(true);
        }
    }
}
