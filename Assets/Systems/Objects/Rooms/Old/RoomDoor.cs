using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;
using static UnityEditor.Progress;

public class RoomDoor : MonoBehaviour, IPlayerInteractableObject
{
    public MeshRenderer meshRenderer;
    public Animator animator;

    public void Awake()
    {
        forwardPoint = transform.position + transform.forward * 1;
        backwardPoint = transform.position + transform.forward * -1;

        currentUserPosition = forwardPoint;
    }

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

    public GameObject doorWallObject;
    public Vector3 currentUserPosition;
    Vector3 forwardPoint;
    Vector3 backwardPoint;

    public void Open(bool open, Vector3 userPosition)
    {
        if (open == true) { currentUserPosition = userPosition; }
        Open(open);
    }
    public void Open(bool open)
    {
        if (IsOpen == open) { return; }
        IsOpen = open;
        

        SoundManager.PlaySound(IsOpen ? openSound : closeSound, soundPlayer);

        doorWallObject.SetActive(open == false);

        animator.SetBool("forwardAnimation", Vector3.Distance(currentUserPosition, forwardPoint) < Vector3.Distance(currentUserPosition, backwardPoint));
        animator.SetTrigger(open ? "open" : "close");

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
        Open(!IsOpen, player.transform.position);
    }


    [Header("Sounds")]
    public SoundPlayer soundPlayer;
    public SoundData openSound;
    public SoundData closeSound;
    public SoundData monsterTryToOpenSound;
    public SoundData creakSound;
    public SoundData breakSound;

}
