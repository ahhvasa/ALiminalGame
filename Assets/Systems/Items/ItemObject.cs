using UnityEngine;

public class ItemObject : MonoBehaviour, IPlayerInteractableObject
{
    public  Item item;

    public void Interact(Player player)
    {
        player.playerInventory.PickUpItem(item);
    }
}
