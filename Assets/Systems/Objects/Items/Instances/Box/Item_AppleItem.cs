public class Item_AppleItem : ItemHoldable
{
    public ObjectSmell objectSmell;
    public override void Activate(bool activateOrDeactivate)
    {

    }
    public override void OnDropInternal()
    {
        objectSmell.AIIgnore = false;
        base.OnDropInternal();
    }
    public override void OnPickUpInternal(Player player)
    {
        objectSmell.AIIgnore = true;
        base.OnPickUpInternal(player);
    }
}
