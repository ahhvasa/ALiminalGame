using System;
using UnityEngine;

public class PlayerDeadState : IPlayerState
{
    private Player player;
    public PlayerDeadState(Player player)
    {
        this.player = player;
    }

    public event Action<bool> OnDeath;

    public void OnEnter()
    {
        Debug.Log("DeadState");
        player.playerMovement.enabled = false;

        player.playerDeath.Die();
        OnDeath?.Invoke(true);
    }
    public void OnExit()
    {
        OnDeath?.Invoke(false);
    }

    public void FixedUpdate()
    {

    }
    public void Update()
    {

    }
}
