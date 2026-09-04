using UnityEngine;

public class Item_Flashlight : ItemHoldable
{
    public bool active;
    public bool on;
    public bool canBeSetToCameraPost;

    public GameObject projectorObject;

    [Header("Sound")]

    public SoundData turnOn;
    public SoundData turnOff;
    //public SoundData flickering;

    //private Sound currentFlickering;


    public override void Activate(bool activateOrDeactivate)
    {
        active = activateOrDeactivate;

        CameraManager.Instance.UpdateCameras(activateOrDeactivate);
    }

    public override void ItemUpdateInternal()
    {
        if (active)
        {
            if (InputProvider.ActivateItem())
            {
                Turn();
            }
        }
    }
    public void Turn(bool onOrOff)
    {
        on = onOrOff;
        Turn();
    }
    public void Turn()
    {
        SoundManager.PlaySound((on == false) ? turnOff : turnOn, playerOwner.soundPlayer);
        //if (on)
        //{
        //    currentFlickering = SoundManager.PlaySound(flickering, playerOwner.soundPlayer);
        //}
        //else
        //{
        //    if (currentFlickering != null) { currentFlickering.DestroySound(); }
        //}

        projectorObject.SetActive(on);
        on = !on;
    }
}

