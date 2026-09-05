using UnityEngine;

public class Touchscreen : MonoBehaviour
{
    public void Start()
    {
        Show(InputManager.Instance.CurrentDevice);
        InputManager.Instance.OnSetDeviceType += Show;
    }

    public void OnDisable()
    {
        InputManager.Instance.OnSetDeviceType -= Show;
    }

    public GameObject panel;

    public void Show(InputDeviceType deviceType)
    {
        if (deviceType == InputDeviceType.TouchScreen)
        {
            panel.SetActive(true);
        }
        else
        {
            panel.SetActive(false);
        }
    }
}