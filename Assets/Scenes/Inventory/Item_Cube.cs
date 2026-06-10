using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class Item_Cube : ItemHoldable
{
    //public override void OnDrop()
    //{
    //    Debug.Log("OnDrop");
    //}

    //public override void OnTakeInHands()
    //{
    //    Debug.Log("OnGetInHands");
    //}

    //public override void OnRemoveFromHands()
    //{
    //    Debug.Log("OnHideFromHands");
    //}

    //public override void OnPickUp()
    //{
    //    Debug.Log("OnPickUp");
    //}
}




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



public abstract class 
    Item : MonoBehaviour
{
    public Player playerOwner;
    public ItemObject itemObject;
    public Texture2D icon;
    public abstract void OnPickUp(Player player);
    public abstract void OnDrop();
    public abstract void OnTakeInHands();
    public abstract void OnRemoveFromHands();


    /// PickUp
    /// Drop
    /// GetInHands
    /// HideFromHands



}
