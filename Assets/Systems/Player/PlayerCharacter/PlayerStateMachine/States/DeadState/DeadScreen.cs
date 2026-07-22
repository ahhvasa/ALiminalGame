using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
    public GameObject deathScreen;
    void Start()
    {
        PlayerStateMachine playerStateMachine = GameObject.FindFirstObjectByType<PlayerStateMachine>();
        playerStateMachine.playerDeadState.OnDeath += ShowDeathScreen;
        ShowDeathScreen(false);
    }

    void ShowDeathScreen(bool show)
    {
        deathScreen.SetActive(show);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
