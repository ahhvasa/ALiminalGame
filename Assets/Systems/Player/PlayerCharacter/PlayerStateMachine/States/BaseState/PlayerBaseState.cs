using UnityEngine;

public class PlayerBaseState : IPlayerState
{
    private Player player;
    public PlayerBaseState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        Debug.Log("BaseState");
        player.playerMovement.enabled = true;
        player.playerInventory.enabled = true;
        player.playerObjectInteraction.enabled = true;
    }
    public void OnExit()
    {
        player.playerMovement.enabled = false;
        player.playerInventory.enabled = false;
        player.playerObjectInteraction.enabled = false;
    }

    public void FixedUpdate()
    {

    }
    public void Update()
    {

    }
}
