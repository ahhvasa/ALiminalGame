using UnityEngine;
using UnityEngine.InputSystem;

public class InputProvider : MonoBehaviour
{
    public static InputProvider Instance;

    public InputActionReference move;
    public InputActionReference interact;
    public InputActionReference drop;
    public InputActionReference activateItem;
    public InputActionReference scroll;
    public InputActionReference crouch; 

    public InputActionReference escape;

    public InputActionReference selectItem_1;
    public InputActionReference selectItem_2;
    public InputActionReference selectItem_3;
    public InputActionReference selectItem_4;

    public InputActionReference rotateCameraLeft;
    public InputActionReference rotateCameraRight;

    public void Awake()
    {
        Instance = this;
    }

    public static Vector3 CurrentMovement()
    {
        Vector3 movement = Instance.move.action.ReadValue<Vector2>();
        movement = new Vector3(movement.x, 0, movement.y);
        return movement;
    }

    public static bool Interact()
    {
        return Instance.interact.action.WasPressedThisFrame();
    }
    public static bool Drop()
    {
        return Instance.drop.action.WasPressedThisFrame();
    }
    public static bool ActivateItem()
    {
        return Instance.activateItem.action.WasPressedThisFrame();
    }
    /// <summary>
    /// return 0 or 1 or -1
    /// </summary>
    /// <returns></returns>
    public static bool MouseScroll(out bool forwardOrBackward)
    {
        float value = Instance.scroll.action.ReadValue<Vector2>().y;

        if (value > 0)
        {
            forwardOrBackward = true;
            return true;
        }
        else if (value < 0)
        {
            forwardOrBackward = false;
            return true;
        }
        else
        {
            forwardOrBackward = false;
            return false;
        }
    }
    public static bool Crouch()
    {
        return Instance.crouch.action.IsPressed();
    }
    public static bool Escape()
    {
        return Instance.escape.action.WasPressedThisFrame();
    }


    public static bool SelectItem_1()
    {
        return Instance.selectItem_1.action.WasPressedThisFrame();
    }
    public static bool SelectItem_2()
    {
        return Instance.selectItem_2.action.WasPressedThisFrame();
    }
    public static bool SelectItem_3()
    {
        return Instance.selectItem_3.action.WasPressedThisFrame();
    }
    public static bool SelectItem_4()
    {
        return Instance.selectItem_4.action.WasPressedThisFrame();
    }

    public static bool RotateCameraLeft()
    {
        return Instance.rotateCameraLeft.action.WasPressedThisFrame();
    }
    public static bool RotateCameraRight()
    {
        return Instance.rotateCameraRight.action.WasPressedThisFrame();
    }

}