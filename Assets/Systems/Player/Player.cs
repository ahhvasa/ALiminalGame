using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerObjectInteraction playerObjectInteraction;
    public PlayerInventory playerInventory;
    public PlayerObjectHold playerObjectHold;
    public PlayerMovement playerMovement;
    public PlayerStateMachine playerStateMachine;

    public void Update()
    {
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
