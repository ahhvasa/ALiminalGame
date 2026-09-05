using UnityEngine;

public class Touchscreen : MonoBehaviour
{
    public void Start()
    {
        Show(InputManager.Instance.CurrentDevice);
        InputManager.Instance.OnSetDeviceType += Show;
    }

    public void Show(InputDeviceType deviceType)
    {
        if (deviceType == InputDeviceType.TouchScreen)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}