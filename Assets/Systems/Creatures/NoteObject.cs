using UnityEngine.Localization;

public class NoteObject : ItemHoldable
{
    public LocalizedString stringReference;

    bool active;
    bool on = false;

    public override void Activate(bool activateOrDeactivate)
    {
        active = activateOrDeactivate;
    }

    public override void ItemUpdateInternal()
    {
        if (active)
        {
            if (InputProvider.ActivateItem())
            {
                Show();
            }
        }
    }

    public void Show()
    {
        on = !on;

        if (on)
        {
            NoteManadger.Instance.ShowNote(stringReference);
        }
        else
        {
            NoteManadger.Instance.HideNote();
        }
    }


}
