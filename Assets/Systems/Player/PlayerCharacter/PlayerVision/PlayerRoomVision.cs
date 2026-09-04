using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerRoomVision : MonoBehaviour
{
    public RoomVision roomVision;
    public bool seeEveryRoom;

    public List<VisibleObject> visibleObjects = new();
    private List<VisibleObject> unVisibleObjects = new();

    void FixedUpdate()
    {
        List<Room> visibleRooms = RoomManager.GetVisibleRooms(transform.position, roomVision);
        List<Room> unVisibleRooms = new List<Room>();

        foreach (Room room in RoomManager.AllRooms)
        {
            if (visibleRooms.Contains(room) == false)
            {
                unVisibleRooms.Add(room);
            }
        }

        visibleObjects.Clear();
        unVisibleObjects = FindObjectsOfType<VisibleObject>().ToList();

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
