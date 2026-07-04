using System.Collections;
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

    public float autoCloseDelay = 1f;

    private Coroutine autoCloseCoroutine;

    public void Open(bool open)
    {
        IsOpen = open;
        meshRenderer.gameObject.SetActive(!open);

        SoundManager.PlaySound(IsOpen ? openSound : closeSound, soundPlayer);


        if (autoCloseCoroutine != null)
        {
            StopCoroutine(autoCloseCoroutine);
            autoCloseCoroutine = null;
        }

        if (open && isActiveAndEnabled)
        {
            autoCloseCoroutine = StartCoroutine(AutoClose());
        }

    }



    private IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(autoCloseDelay);

        autoCloseCoroutine = null;
        Open(false);
    }

    private void OnDisable()
    {
        if (autoCloseCoroutine != null)
        {
            StopCoroutine(autoCloseCoroutine);
            autoCloseCoroutine = null;
        }
    }




    public void Interact(Player player)
    {
        Open(!IsOpen);
    }


    [Header("Sounds")]
    public SoundPlayer soundPlayer;
    public SoundData openSound;
    public SoundData closeSound;
    public SoundData monsterTryToOpenSound;
    public SoundData creakSound;
    public SoundData breakSound;

}
