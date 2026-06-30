using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadScreen : MonoBehaviour
{
    public GameObject deathScreen;
    void Start()
    {
        PlayerStateMachine playerStateMachine = GameObject.FindFirstObjectByType<PlayerStateMachine>();
        playerStateMachine.playerDeadState.OnDeath += ShowDeathScreen;
    }

    void ShowDeathScreen(bool show)
    {
        deathScreen.SetActive(show);
    }
}
