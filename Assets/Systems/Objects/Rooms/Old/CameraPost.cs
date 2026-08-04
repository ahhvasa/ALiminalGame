using UnityEngine;



public class CameraPost : MonoBehaviour, IPlayerInteractableObject
{
    public Animator animator;
    public Item currentItem;
    public Transform objectParent;

    public void Awake()
    {
        animator.enabled = false;
    }

    public void Interact(Player player)
    {
        if (player.playerInventory.CurrentItem == null)
        {
            if (currentItem != null) { GiveItem(player); }
        }
        else
        {
            if (currentItem == null) { TakeItem(player); }
        }
    }

    public void GiveItem(Player player)
    {
        animator.enabled = false;
        if (currentItem is Item_CCTVCamera)
        {
            (currentItem as Item_CCTVCamera).Working = false;
        }

        currentItem.itemObject.transform.SetParent(null);
        player.playerInventory.PickUpItem(currentItem);
        currentItem = null;
    }

    public void TakeItem(Player player)
    {
        if (player.playerInventory.CurrentItem is Item_CCTVCamera == false
            && player.playerInventory.CurrentItem is Item_Flashlight == false) { return; }

        animator.enabled = true;
        currentItem = player.playerInventory.TakeItem();

        currentItem.itemObject.transform.SetParent(objectParent);
        currentItem.itemObject.transform.localPosition = Vector3.zero;
        currentItem.itemObject.transform.localRotation = Quaternion.identity;

        if (currentItem is Item_CCTVCamera)
        {
            (currentItem as Item_CCTVCamera).Working = true;
        }
        if (currentItem is Item_Flashlight)
        {
            (currentItem as Item_Flashlight).Turn(true);
        }
    }
}