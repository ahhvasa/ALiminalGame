using UnityEngine;

public class Item_Flashlight : ItemHoldable
{
    public bool active;
    public bool on;

    public GameObject projectorObject;

    [Header("Sound")]

    public SoundPlayer soundPlayer;

    public SoundData turnOn;
    public SoundData turnOff;
    public SoundData_RandomSound flickering;

    private Sound currentFlickering;


    public override void Activate(bool activateOrDeactivate)
    {
        active = activateOrDeactivate;
    }

    public void Update()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Turn();
            }
        }
    }

    public void Turn()
    {
        SoundManager.PlaySound((on == false) ? turnOff : turnOn, soundPlayer);
        if (on)
        {
            currentFlickering = SoundManager.PlaySound(flickering, soundPlayer);
        }
        else
        {
            if (currentFlickering != null) { currentFlickering.End(); }
        }

        projectorObject.SetActive(on);
        on = !on;
    }
}
