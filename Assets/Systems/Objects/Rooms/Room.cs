using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public VisibleObject visibleObject;
    public Transform roomCenter;
    public RoomConnectedPart[] roomParts;
    public RoomZone roomZone;

    public RoomView roomView;

    public void Update()
    {

    }

    public void ApplyTextures()
    {
        foreach (var part in roomParts)
        {
            part.roomPart.SetWallTexture(this, roomView.material);
        }
    }

    public RoomConnectedPart[] GetRoomConnectedParts()
    {
        return roomParts;
    }

    public List<Room> GetAdjacentRooms()
    {
        List<Room> rooms = new List<Room>();
        rooms.Add(this);
        foreach (var roomPart in roomParts)
        {
            if (roomPart.TryGetConnectedRoom(out Room room))
            {
                rooms.Add(room);
            }
        }
        return rooms;
    }

    public List<Room> GetAdjacentVisibleRooms()
    {
        List<Room> rooms = new List<Room>();
        rooms.Add(this);

        foreach (var roomPart in roomParts)
        {
            if (roomPart.CanSeeConnectedRoom())
            {
                if (roomPart.TryGetConnectedRoom(out Room room))
                {
                    rooms.Add(room);
                }
            }
        }
        return rooms;
    }

    public List<Room> GetAllVisibleRooms()
    {
        List<Room> allRooms = GetAdjacentVisibleRooms();

        for (int i = 0; i != allRooms.Count; i++)
        {
            var rooms = allRooms[i].GetAdjacentVisibleRooms();

            foreach (var room in rooms)
            {
                if (allRooms.Contains(room)) { continue; }
                allRooms.Add(room);
            }
        }
        return allRooms;
    }


    public void Show(bool show)
    {
        visibleObject.Show(show);
        roomZone.Show(show);
    }

    public List<VisibleObject> GetAllVisibleObjects()
    {
        List<VisibleObject> allVisibleObjects = new List<VisibleObject>();
        allVisibleObjects.AddRange(roomZone.visibleObjects);
        allVisibleObjects.Add(visibleObject);
        return allVisibleObjects;
    }
}


[Serializable]
public class RoomView
{
    public Material material;
}