using UnityEngine;

public abstract class 
    Item : MonoBehaviour
{
    public Player playerOwner;
    public ItemObject itemObject;
    public Sprite icon;
    public abstract void OnPickUp(Player player);
    public abstract void OnDrop();
    public abstract void OnTakeInHands();
    public abstract void OnRemoveFromHands();


    /// <summary>
    /// taking or removing from hands
    /// </summary>when 
    public abstract void Activate(bool activateOrDeactivate);
}
