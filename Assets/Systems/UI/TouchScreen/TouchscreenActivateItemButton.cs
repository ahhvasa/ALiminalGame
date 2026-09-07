using UnityEngine;
using UnityEngine.UI;

public class TouchscreenActivateItemButton : MonoBehaviour
{
    public Image image;
    public Sprite defaultSprite;
    private bool eventIsSet = false;

    public void Start()
    {
        if (eventIsSet == false)
        {
            var playerInventory = GameObject.FindObjectOfType<Player>().playerInventory;
            playerInventory.OnTakeInHands += ShowItemIcon;
            playerInventory.OnPickUp += ShowItemIcon;
            playerInventory.OnDrop += (Item item, int index) => { image.sprite = defaultSprite; };

            eventIsSet = true;
        }
    }

    public void ShowItemIcon(Item item, int index)
    {
        if (item == null) 
        {
            image.sprite = defaultSprite;
            return; 
        }
        image.sprite = item.icon;
    }
}