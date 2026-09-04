using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    public void Update()
    {
        UseController();
        //if (Mouse.current != null)
        //{
        //    UseMouse();
        //}
        //else
        //{
        //    UseController();
        //}
    }

    public void UseMouse()
    {
        LookAtDirection(GroundQuad.Point);
    }
    public void UseController()
    {
        Vector3 lookDirection = InputProvider.CurrentLookDirection();

        Debug.Log("Look at " + lookDirection);

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