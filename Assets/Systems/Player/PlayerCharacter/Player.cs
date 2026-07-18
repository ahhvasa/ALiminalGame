using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerObjectInteraction playerObjectInteraction;
    public PlayerInventory playerInventory;
    public PlayerObjectHold playerObjectHold;
    public PlayerMovement playerMovement;
    public PlayerStateMachine playerStateMachine;
    public PlayerRoomVision roomVision;
    public PlayerMonsterInView playerMonsterInView;

    public SoundPlayer soundPlayer;

    public void Update()
    {
    }

    public void Die()
    {
        playerStateMachine.EnterState(playerStateMachine.playerDeadState);
    }

    public void BaseState()
    {
        playerStateMachine.EnterState(playerStateMachine.playerBaseState);
    }

    public void EnterState(IPlayerState state)
    {
        playerStateMachine.EnterState(state);
    }
}
