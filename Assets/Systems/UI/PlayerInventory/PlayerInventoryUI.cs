using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    public static PlayerInventoryUI Instance;

    public PlayerInventory playerInventory;
    public PlayerInventoryUI_Item[] items;

    public GameObject inventoryPanel;

    public void Awake()
    {
        Instance = this;
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

    public void ShowInventoryPanel(bool showOrHide)
    {
        inventoryPanel.SetActive(showOrHide);
    }
}
