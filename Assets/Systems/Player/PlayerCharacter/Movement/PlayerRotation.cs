using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    public void Update()
    {
        if (InputManager.Instance.CurrentDevice == InputDeviceType.KeyboardMouse)
        {
            UseMouse();
        }
        else
        {
            UseController();
        }
    }

    public void UseMouse()
    {
        LookAtDirection(GroundQuad.Point);
    }

    public void UseController()
    {
        Vector3 lookDirection = InputProvider.CurrentLookDirection();

        if (lookDirection.magnitude <= 0.2f)
        {
            LookAtDirection(transform.position + InputProvider.CurrentMovement());
        }
        else
        {
            LookAtDirection(transform.position + lookDirection.normalized);
        }
    }


    public void LookAtDirection(Vector3 point)
    {
        transform.LookAt(point, Vector3.up);
    }
}