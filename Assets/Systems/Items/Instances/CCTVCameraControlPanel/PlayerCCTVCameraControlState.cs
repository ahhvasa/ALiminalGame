using System.Collections.Generic;
using UnityEngine;

public class PlayerCCTVCameraControlState : IPlayerState
{
    private Player player;
    public PlayerCCTVCameraControlState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        Debug.Log("PlayerCCTVCameraControlState");
        player.playerMovement.enabled = false;
    }
    public void OnExit()
    {

    }

    public void FixedUpdate()
    {
    }
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            ExitCameraControlState();
        }
    }



    public void ExitCameraControlState()
    {
        player.BaseState();
    }
}

