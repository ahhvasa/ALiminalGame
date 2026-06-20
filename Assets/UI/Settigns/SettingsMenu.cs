using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsMenu : MonoBehaviour
{
    private const string SettingsKey = "Settings";

    [Inject] private ISaveSystem saveSystem;

    [SerializeField] public Button backButton;

    [Header("Audio")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    [Header("Resolution")]
    [SerializeField] private Dropdown resolutionDropdown;
    private List<Resolution> resolutions = new List<Resolution>();

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
    }

    private void UnsubscribeEvents()
    {
        sfxSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
        musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);

        applyButton.onClick.RemoveListener(OnApplyClicked);
        defaultsButton.onClick.RemoveListener(OnDefaultsClicked);
        cancelButton.onClick.RemoveListener(OnCancelClicked);

        resolutionDropdown.onValueChanged.RemoveListener(OnResolutionChange);
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
        return GameOptionsData.Create(Screen.currentResolution, sfxVolume: 1, musicVolume: 1);

        //return new GameOptionsData
        //{
        //    sfxVolume = 1f,
        //    musicVolume = 1f
        //};
    }
}