using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    private bool active;

    public GameObject gameMenuPanel;
    public SettingsMenu settingsMenu;

    public Button continueButton;
    public Button optionsButton;
    public Button restartButton;
    public Button exitButton;

    public GameObject restartConfirmPanel;
    public Button restartConfirm_yes;
    public Button restartConfirm_no;

    public void Start()
    {
        SubscribeEvents(); 
    }

    public void SubscribeEvents()
    {
        continueButton.onClick.AddListener(Continue);
        optionsButton.onClick.AddListener(Options);
        exitButton.onClick.AddListener(Exit);
        restartButton.onClick.AddListener(Restart);

        restartConfirm_yes.onClick.AddListener(() => RestartConfirm(true));
        restartConfirm_no.onClick.AddListener(() => RestartConfirm(false));

        settingsMenu.backButton.onClick.AddListener(OptionsClose);
    }

    public void Update()
    {
        if (InputProvider.Escape())
        {
            if (active)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void Continue()
    {
        CloseMenu();
    }    

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        gameMenuPanel.SetActive(false);
        restartConfirmPanel.SetActive(true);
    }
    public void RestartConfirm(bool confirmOrNo)
    {
        restartConfirmPanel.SetActive(false);
        gameMenuPanel.SetActive(true);

        if (confirmOrNo)
        {
            CloseMenu();
            SceneLoader.Instance.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
    }

    public void Options()
    {
        gameMenuPanel.SetActive(false);
        settingsMenu.settingsMenuPanel.SetActive(true);
    }
    public void OptionsClose()
    {
        gameMenuPanel.SetActive(true);
        settingsMenu.settingsMenuPanel.SetActive(false);
    }

    public void OpenMenu()
    {
        Time.timeScale = 0;
        gameMenuPanel.SetActive(true);
        settingsMenu.settingsMenuPanel.SetActive(false);
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        gameMenuPanel.SetActive(false);
        settingsMenu.settingsMenuPanel.SetActive(false);
    }

}
