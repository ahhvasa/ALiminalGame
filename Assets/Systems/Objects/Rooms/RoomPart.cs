using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public abstract class RoomPart : MonoBehaviour
{
    public List<Room> rooms;

    public abstract bool CanWalkToConnectedRoom(Room hostRoom);
    public abstract bool CanSeeConnectedRoom(Room hostRoom);

    public virtual bool TryGetConnectedRoom(Room hostRoom, out Room connectedRoom)
    {
        foreach (var room in rooms)
        {
            if (room != hostRoom) { connectedRoom = room; return true; }
        }

        connectedRoom = null;
        return false;
    }

    public void SetWallTexture(Room hostRoom, Texture2D texture)
    {
        RoomObjectWall[] roomObjectWalls = GetComponentsInChildren<RoomObjectWall>();
        foreach (var roomObjectWall in roomObjectWalls)
        {
            roomObjectWall.SetTexture(hostRoom, texture);
        }
    }
}
