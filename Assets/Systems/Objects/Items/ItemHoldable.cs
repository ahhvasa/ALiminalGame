using UnityEngine;

public abstract class ItemHoldable : Item
{

    public override void OnPickUpInternal(Player player)
    {
        playerOwner.playerObjectHold.HoldObject(itemObject.transform);
        itemObject.transform.localRotation = Quaternion.identity;

    }
    public override void OnDropInternal()
    {
        playerOwner.playerObjectHold.DropObject();
    }

    public override void OnTakeInHandsInternal()
    {
        playerOwner.playerObjectHold.HoldObject(itemObject.transform);
        itemObject.gameObject.SetActive(true);
        itemObject.transform.localRotation = Quaternion.identity;
    }

    public override void OnRemoveFromHandsInternal()
    {
        itemObject.gameObject.SetActive(false);
    }

}
