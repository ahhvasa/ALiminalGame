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


            if (working)
            {
                CCTVCameraManager.Instance.activeCameras.Add(this);
            }
            else
            {
                if (CCTVCameraManager.Instance == null) { return; }
                CCTVCameraManager.Instance.activeCameras.Remove(this);
            }
        }
    }
    public void Start()
    {
        base.Start();
        Working = false;
    }

    public GameObject lightObject;

    public bool active;

    public override void Activate(bool activateOrDeactivate)
    {
        active = activateOrDeactivate;
        CameraManadger.Instance.UpdateCameras(activateOrDeactivate);

    }

    public void Update()
    {

    }
}

