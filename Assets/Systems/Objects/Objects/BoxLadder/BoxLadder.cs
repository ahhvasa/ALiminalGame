using UnityEngine;
using System.Collections.Generic;
using System;

public class BoxLadder : MonoBehaviour, IPlayerInteractableObject
{
    private int boxCount;
    private int boxReqiered;

    public List<GameObject> boxElements;
    public GameObject ladderObject;


    public Action OnBuild;

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
                ladderObject.SetActive(true);
                OnBuild?.Invoke();
            }
            else
            {
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
