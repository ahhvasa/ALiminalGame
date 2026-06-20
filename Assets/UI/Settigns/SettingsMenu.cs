using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Localization.Editor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using Zenject;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsMenuPanel;

    private const string SettingsKey = "Settings";

    [Inject] private ISaveSystem saveSystem;

    [SerializeField] public Button backButton;

    [Header("Audio")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    [Header("Resolution")]
    [SerializeField] private Dropdown resolutionDropdown;
    private List<Resolution> resolutions = new List<Resolution>();


    [Header("Localization")]
    [SerializeField] private Dropdown languageDropdown;

    [Header("Buttons")]
    [SerializeField] private Button applyButton;
    [SerializeField] private Button defaultsButton;
    [SerializeField] private Button cancelButton;

    [SerializeField] private GameOptionsData options;

    private async void Start()
    {
        InitializeUI();
        await LoadOptions();
        SubscribeEvents();
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void UpdateUI()
    {
        sfxSlider.SetValueWithoutNotify(options.sfxVolume);
        musicSlider.SetValueWithoutNotify(options.musicVolume);
        resolutionDropdown.SetValueWithoutNotify(ResolutionToId(options.resolution));
        languageDropdown.SetValueWithoutNotify(LanguageToId(options.language));
    }

    private async Task LoadOptions()
    {
        options = await saveSystem.LoadAsync<GameOptionsData>(SettingsKey);
        options ??= CreateDefaultOptions();
        await ApplySettings();
    }

    private async Task SaveOptions()
    {
        await saveSystem.SaveAsync(SettingsKey, options);
    }

    private async Task ApplySettings()
    {
        options.ApplySettings();
        UpdateUI();
        await SaveOptions();
    }

    #region UI Initialization


    private void InitializeUI()
    {
        InitializeResolutions();
        InitializeLanguages();
    }
    private void InitializeResolutions()
    {
        Resolution[] resolutionsRaw = Screen.resolutions;
        List<string> options = new();

        foreach (Resolution resolutionRaw in resolutionsRaw)
        {
            bool alreadyExists = resolutions.Exists(r =>
                r.width == resolutionRaw.width &&
                r.height == resolutionRaw.height);

            if (alreadyExists)
                continue;

            resolutions.Add(resolutionRaw);

            options.Add($"{resolutionRaw.width} x {resolutionRaw.height}");
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = 0;
        resolutionDropdown.RefreshShownValue();
    }

    private void InitializeLanguages()
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;

        languageDropdown.ClearOptions();

        languageDropdown.AddOptions(
            locales
                .Select(locale => locale.LocaleName)
                .ToList()
        );

        languageDropdown.value = locales.IndexOf(
            LocalizationSettings.SelectedLocale
        );
    }
    #endregion


    #region Event Subscription

    private void SubscribeEvents()
    {
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);

        applyButton.onClick.AddListener(OnApplyClicked);
        defaultsButton.onClick.AddListener(OnDefaultsClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChange);
        languageDropdown.onValueChanged.AddListener(OnLanguageChange);
    }

    private void UnsubscribeEvents()
    {
        sfxSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
        musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);

        applyButton.onClick.RemoveListener(OnApplyClicked);
        defaultsButton.onClick.RemoveListener(OnDefaultsClicked);
        cancelButton.onClick.RemoveListener(OnCancelClicked);

        resolutionDropdown.onValueChanged.RemoveListener(OnResolutionChange);
        languageDropdown.onValueChanged.AddListener(OnLanguageChange);
    }

    #endregion

    #region UI Event Handlers

    private void OnSfxVolumeChanged(float value)
    {
        options.sfxVolume = value;
    }

    private void OnMusicVolumeChanged(float value)
    {
        options.musicVolume = value;
    }

    private void OnResolutionChange(int index)
    {
        options.resolution = resolutions[index];
    }

    private void OnLanguageChange(int index)
    {
        options.language = LocalizationSettings.AvailableLocales.Locales[index];
    }

    private async void OnApplyClicked()
    {
        await ApplySettings();
    }

    private async void OnDefaultsClicked()
    {
        await SetDefault();
    }

    private async void OnCancelClicked()
    {
        await Cancel();
    }


    #endregion

    #region Other

    private int ResolutionToId(Resolution resolution)
    {
        for (int i = 0; i != resolutions.Count; i++)
        {
            if (resolutions[i].height == resolution.height &&
                resolutions[i].width == resolution.width)
            {
                return i;
            }
        }
        Debug.LogError("No resolution " + resolution.ToString());
        return 0;
    }

    private int LanguageToId(Locale locale)
    {
        for (int i = 0; i != LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            Debug.Log($"count =  {LocalizationSettings.AvailableLocales.Locales.Count},  cur = {locale}, our = {LocalizationSettings.AvailableLocales.Locales[i]}");
            if (locale.Identifier.Code == LocalizationSettings.AvailableLocales.Locales[i].Identifier.Code)
            {
                return i;
            }
        }
        Debug.LogError("No locale " + locale.ToString());
        return 0;
    }

    #endregion

    private async Task Cancel()
    {
        await LoadOptions();
    }

    private async Task SetDefault()
    {
        options = CreateDefaultOptions();
        await ApplySettings();
    }

    private GameOptionsData CreateDefaultOptions()
    {
        return GameOptionsData.Create(Screen.currentResolution, sfxVolume: 1, musicVolume: 1, language: LocalizationSettings.AvailableLocales.Locales[0]);

        //return new GameOptionsData
        //{
        //    sfxVolume = 1f,
        //    musicVolume = 1f
        //};
    }
}