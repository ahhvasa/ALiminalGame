using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
    public GameMenu gameMenu;
    public GameObject deathScreen;

    void Start()
    {
        PlayerStateMachine playerStateMachine = GameObject.FindFirstObjectByType<PlayerStateMachine>();
        playerStateMachine.playerDeadState.OnDeath += ShowDeathScreen;

        ShowDeathScreen(false);
    }

    void ShowDeathScreen(bool show)
    {
        gameMenu.enabled = !show;
        deathScreen.SetActive(show);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneLoader.Instance.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }
}
