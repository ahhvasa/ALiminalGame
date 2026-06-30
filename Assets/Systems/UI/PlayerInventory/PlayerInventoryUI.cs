using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public PlayerInventoryUI_Item[] items;

    public void Start()
    {
        playerInventory.OnPickUp += SetTextureToItem;
        playerInventory.OnDrop += HideItem;

        playerInventory.OnSetSlot += SetSlot;
    }

    public void SetTextureToItem(Item item, int id)
    {
        items[id].CurrentSprite = item.icon;
    }
    public void HideItem(Item item, int id)
    {
        items[id].CurrentSprite = null;
    }

    public void SetSlot(int id)
    {
        foreach (PlayerInventoryUI_Item item in items)
        {
            item.activeBorder.SetActive(false);
        }
        items[id].activeBorder.SetActive(true);
    }
}
