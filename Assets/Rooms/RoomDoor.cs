using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour, IPlayerInteractableObject
{
    public MeshRenderer meshRenderer;

    public List<Room> GetRooms()
    {
        List<Room> rooms = new List<Room>();
        foreach(RoomDoorWay doorWay in doorWays)
        {
            rooms.Add(doorWay.room);
        }
        return rooms;
    }
    public List<RoomDoorWay> doorWays;

    public bool IsOpen;

    public void Open(bool open)
    {
        IsOpen = open;
        meshRenderer.gameObject.SetActive(!open);
    }

    public void Interact(Player player)
    {
        Open(!IsOpen);
    }

}
