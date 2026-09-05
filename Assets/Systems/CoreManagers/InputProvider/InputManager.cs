using System;
using UnityEngine;
using UnityEngine.Device;
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
            OnSetDeviceType?.Invoke(_currentDevice);
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
        if (AllowEditing() == false)
        { return; }

        if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
            return;

        if (device is Mouse || device is Keyboard)
        { SetDevice(InputDeviceType.KeyboardMouse); }
        if (device is Gamepad)
        { SetDevice(InputDeviceType.Gamepad); }
        if (device.layout.Contains("Touchscreen"))
        { SetDevice(InputDeviceType.TouchScreen); }

        Debug.Log($"Device: {device.name},  type {device.GetType()}");
    }

    private bool AllowEditing()
    {
        if (CurrentDevice == InputDeviceType.TouchScreen)
        {
            return false;
        }

        return true;
    }

    private void SetDevice(InputDeviceType device)
    {
        if (AllowEditing() == false)
        { return; }

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
