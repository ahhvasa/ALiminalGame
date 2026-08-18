public class Item_AppleItem : ItemHoldable
{
    public ObjectSmell objectSmell;

    public new void Start()
    {
        base.Start();
        objectSmell.AIIgnore = false;
    }

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
