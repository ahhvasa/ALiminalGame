using System.Collections;
using System.Collections.Generic;
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

        foreach (Room room in unVisibleRooms)
        {
            if (seeEveryRoom) { room.Show(true); return; }
            room.Show(false);
        }
        foreach (Room room in visibleRooms)
        {
            room.Show(true);
        }
    }
}
