using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A component for player movement. It immediately checks for key presses and applies movement. If movement needs to be disabled, set component.enabled to false.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 10f;
    public float crouchSpeed = 5f;
    public float currentMaxSpeed = 5f;

    public FootstepAudio_DiffrentSoundsForMaterials footstepAudio;

    private Vector3 _velocity;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// normalized direction and speed factor from 0 to 1
    /// </summary>
    public event Action<Vector3, float> OnMove;

    /// <summary>
    /// normalized direction and speed factor from 0 to 1
    /// </summary>
    public void Move(float speedFactor, Vector3 direction)
    {
        speedFactor = Mathf.Clamp01(speedFactor);

        direction.y = 0f;
        direction = direction.normalized;

        _velocity = direction * (currentMaxSpeed * speedFactor);
        rb.velocity = _velocity;

        OnMove?.Invoke(direction, speedFactor);
    }

    public void FixedUpdate()
    {
        SetCrouchMovement(InputProvider.Crouch());
    }

    public void Update()
    {
        Vector3 inputDirection = InputProvider.CurrentMovement();

        float speed = 0;

        if (inputDirection.magnitude > 0f) { speed = 1; }
        else
        {
            Move(0f, Vector3.zero);
            return;
        }


        Transform camera = Camera.main.transform;

        Vector3 camForward = camera.forward;
        Vector3 camRight = camera.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 direction = camForward * inputDirection.z + camRight * inputDirection.x;

        Move(speed, direction);
    }

    public void SetCrouchMovement(bool setCrouchSpeed)
    {
        currentMaxSpeed = setCrouchSpeed ? crouchSpeed : runSpeed;
        footstepAudio.blockSound = setCrouchSpeed;
    }

    private void OnDisable()
    {
        Move(0f, Vector3.zero);
    }
}
