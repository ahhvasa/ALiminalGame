using UnityEngine;

public abstract class ItemHoldable : Item
{

    public override void OnDrop()
    {
        playerOwner.playerObjectHold.DropObject();
        playerOwner = null;

        Activate(false);
    }

    public override void OnTakeInHands()
    {
        playerOwner.playerObjectHold.HoldObject(itemObject.transform);
        itemObject.gameObject.SetActive(true);

        Activate(true);
    }

    public override void OnRemoveFromHands()
    {
        itemObject.gameObject.SetActive(false);

        Activate(false);
    }

    public override void OnPickUp(Player player)
    {
        playerOwner = player;
        playerOwner.playerObjectHold.HoldObject(itemObject.transform);

        Activate(true);
    }
}
