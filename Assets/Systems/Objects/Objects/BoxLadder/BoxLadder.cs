using UnityEngine;
using System.Collections.Generic;
using System;

public class BoxLadder : MonoBehaviour, IPlayerInteractableObject
{

    private int boxCount;
    private int boxReqiered;

    public List<GameObject> boxElements;
    public GameObject ladderObject;

    public SoundData sound;

    public ObjectTextLabel textLabel;

    public Action OnBuild;
    public InteractableObjectFlag interactableObjectFlag;

    public void Start()
    {
        interactableObjectFlag.active = false;
    }

    public int BoxCount
    {
        get { return boxCount; }
        set
        {
            boxCount = value;
            for (int i = 0; i != boxElements.Count; i++)
            {
                boxElements[i].SetActive(i < boxCount);
            }

            if (boxCount >= boxReqiered)
            {
                SoundManager.PlaySound(sound, attachedPortalDoor.roomDoor.soundPlayer);
                ladderObject.SetActive(true);
                textLabel.Text = "";
                OnBuild?.Invoke();
            }
            else
            {
                textLabel.Text = $"{boxCount}/{boxReqiered}";
                ladderObject.SetActive(false);
            }

            
        }
    }

    public DoorAutoOpenForPlayer attachedPortalDoor;

    public void Awake()
    {
        boxReqiered = boxElements.Count;
        BoxCount = 0;
        OnBuild += () => attachedPortalDoor.enabled = true;

        FindObjectOfType<PlayerInventory>().OnTakeInHands += BoxLadder_OnTakeInHands;
        FindObjectOfType<PlayerInventory>().OnPickUp += BoxLadder_OnTakeInHands;
    }

    private void BoxLadder_OnTakeInHands(Item arg1, int arg2)
    {
        if (arg1 is Item_StandartItem)
        {
            Item_StandartItem item = arg1 as Item_StandartItem;
            if (item.canBeUsedToBuildLadder)
            {
                interactableObjectFlag.active = true;
            }
            else
            {
                interactableObjectFlag.active = false;
            }
        }
        else
        {
            interactableObjectFlag.active = false;
        }
    }

    public void Interact(Player player)
    {
        if (player.playerInventory.CurrentItem is Item_StandartItem)
        {
            if ((player.playerInventory.CurrentItem as Item_StandartItem).canBeUsedToBuildLadder == false) { return; }

            Item_StandartItem item = player.playerInventory.TakeItem() as Item_StandartItem;
            item.itemObject.transform.position = new Vector3(0, -999, 0);
            item.itemObject.gameObject.SetActive(false);

            BoxCount += 1;
        }
    }
}
