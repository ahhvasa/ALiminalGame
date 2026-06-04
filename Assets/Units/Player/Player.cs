using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerObjectInteraction playerObjectInteraction;
    public PlayerMovement playerMovement;
    public PlayerStateMachine playerStateMachine;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Die();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BaseState();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerObjectInteraction.Interact();
        }

    }

    public void Die()
    {
        playerStateMachine.EnterState(playerStateMachine.playerDeadState);
    }

    public void BaseState()
    {
        playerStateMachine.EnterState(playerStateMachine.playerBaseState);
    }
}
