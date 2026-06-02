using UnityEngine;

public class PlayerDeadState : IPlayerState
{
    private Player player;
    public PlayerDeadState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        Debug.Log("DeadState");
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

    }
}
