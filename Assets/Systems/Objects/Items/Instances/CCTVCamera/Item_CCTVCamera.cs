public class Item_CCTVCamera : ItemHoldable
{
    public bool active;

    public override void Activate(bool activateOrDeactivate)
    {
        active = activateOrDeactivate;
    }

    public void Update()
    {

    }
}

