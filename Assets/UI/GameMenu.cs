using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    private bool active;

    public GameObject gameMenuPanel;
    public SettingsMenu settingsMenu;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    public void Start()
    {
        SubscribeEvents(); 
    }

    public void SubscribeEvents()
    {
        continueButton.onClick.AddListener(Continue);
        optionsButton.onClick.AddListener(Options);
        exitButton.onClick.AddListener(Exit);

        settingsMenu.backButton.onClick.AddListener(OptionsClose);
    }

    public void UnsubscribeEvents()
    {
        continueButton.onClick.RemoveListener(Continue);
        optionsButton.onClick.RemoveListener(Options);
        exitButton.onClick.RemoveListener(Exit);

        settingsMenu.backButton.onClick.RemoveListener(OptionsClose);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Debug.Log("Exit");
    }

    public void Options()
    {
        gameMenuPanel.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
    }
    public void OptionsClose()
    {
        gameMenuPanel.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        Time.timeScale = 0;
        gameMenuPanel.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        gameMenuPanel.SetActive(false);
        settingsMenu.gameObject.SetActive(false);
    }

}
