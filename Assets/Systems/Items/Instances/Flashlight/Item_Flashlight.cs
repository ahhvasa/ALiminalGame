using UnityEngine;

public class Item_Flashlight : ItemHoldable
{
    public bool active;
    public bool on;

    public GameObject projectorObject;

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
        projectorObject.SetActive(on);
        on = !on;
    }
}
