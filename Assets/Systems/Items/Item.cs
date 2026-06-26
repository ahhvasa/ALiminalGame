using System;
using UnityEngine;

public abstract class 
    Item : MonoBehaviour
{
    public Player playerOwner;
    public ItemObject itemObject;
    public Sprite icon;
    public itemTextureType textureType;
    private bool _isInInventory = false;
    public bool IsInInventory { get { return _isInInventory; } }

    public abstract void OnPickUpInternal(Player player);
    public abstract void OnDropInternal();
    public abstract void OnTakeInHandsInternal();
    public abstract void OnRemoveFromHandsInternal();
    public virtual void ItemFixedUpdateInternal() { }
    public virtual void ItemUpdateInternal() { }

    public void OnPickUp(Player player)
    {
        playerOwner = player;
        _isInInventory = true;

        Activate(true);

        SoundManager.PlaySound(SoundManager.Instance.itemSounds.GetPickUpSound(textureType), playerOwner.soundPlayer);

        OnPickUpInternal(player);
    }
    public void OnDrop()
    {
        SoundManager.PlaySound(SoundManager.Instance.itemSounds.itemDropDown, playerOwner.soundPlayer);
        _isInInventory = false;

        OnDropInternal();

        Activate(false);
        playerOwner = null;
    }
    public void OnTakeInHands()
    {
        Activate(true);

        SoundManager.PlaySound(SoundManager.Instance.itemSounds.GetPickUpSound(textureType), playerOwner.soundPlayer);

        OnTakeInHandsInternal();
    }
    public void OnRemoveFromHands()
    {
        Activate(false);

        OnRemoveFromHandsInternal();
    }
    public void ItemFixedUpdate()
    {
        ItemFixedUpdateInternal();
    }
    public void ItemUpdate()
    {
        ItemUpdateInternal();
    }

    /// <summary>
    /// taking or removing from hands
    /// </summary>when 
    public abstract void Activate(bool activateOrDeactivate);
}

public enum itemTextureType
{
    bulky,
    cardboard,
    standart,
    plastic,
    rag,
    wood
}