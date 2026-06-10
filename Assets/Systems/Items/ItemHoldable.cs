using UnityEngine;

public class ItemHoldable : Item
{

    public override void OnDrop()
    {
        playerOwner.playerObjectHold.DropObject();
        playerOwner = null;
        Debug.Log("OnDrop");
    }

    public override void OnTakeInHands()
    {
        playerOwner.playerObjectHold.HoldObject(itemObject.transform);
        itemObject.gameObject.SetActive(true);
        Debug.Log("OnGetInHands");
    }

    public override void OnRemoveFromHands()
    {
        playerOwner.playerObjectHold.DropObject();
        itemObject.gameObject.SetActive(false);
        Debug.Log("OnHideFromHands");
    }

    public override void OnPickUp(Player player)
    {
        playerOwner = player;
        playerOwner.playerObjectHold.HoldObject(itemObject.transform);
        Debug.Log("OnPickUp");
    }
}
