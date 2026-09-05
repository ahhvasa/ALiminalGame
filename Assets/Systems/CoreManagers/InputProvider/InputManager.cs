using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private InputDeviceType _currentDevice; 
    public InputDeviceType CurrentDevice
    {
        get
        {
            return _currentDevice;
        }
        set
        {
            if (_currentDevice == value)
            {
                return;
            }
            _currentDevice = value;
            OnSetDeviceType.Invoke(_currentDevice);
        }
    }

    public event Action<InputDeviceType> OnSetDeviceType;

    public void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        InputSystem.onEvent += OnInputEvent;
    }

    private void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }

    private void OnInputEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
            return;

        if (device is Mouse || device is Keyboard)
        { SetDevice(InputDeviceType.KeyboardMouse); }
        else if (device is Gamepad)
        { SetDevice(InputDeviceType.Gamepad); }
        else if (device is Touchscreen)
        { SetDevice(InputDeviceType.TouchScreen); }
    }

    private void SetDevice(InputDeviceType device)
    {
        CurrentDevice = device;
        Debug.Log($"Input device: {device}");
    }
}

public enum InputDeviceType
{
    KeyboardMouse,
    Gamepad,
    TouchScreen
}
