using UnityEngine;

public class Item_CCTVCamera : ItemHoldable
{
    [SerializeField] private bool working;
    public bool Working
    {
        get { return working; }
        set 
        {
            lightObject.SetActive(value);
            working = value; 
        }
    }
    public void Awake()
    {
        Working = false;
    }

    public GameObject lightObject;

    public bool active;

    public override void Activate(bool activateOrDeactivate)
    {
        active = activateOrDeactivate;
    }

    public void Update()
    {

    }
}

