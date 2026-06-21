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

        SoundManager.PlaySound(IsOpen ? openSound : closeSound, soundPlayer);
        Creak();
    }

    public void Interact(Player player)
    {
        Open(!IsOpen);
    }

    public void Creak()
    {
        if (Random.Range(0,100) <= 30)
        {
            SoundManager.PlaySound(creakSound, soundPlayer);
        }
    }

    [Header("Sounds")]
    public SoundPlayer soundPlayer;
    public SoundData_RandomSound openSound;
    public SoundData_RandomSound closeSound;
    public SoundData_RandomSound monsterTryToOpenSound;
    public SoundData_RandomSound creakSound;
    public SoundData_RandomSound breakSound;

}
