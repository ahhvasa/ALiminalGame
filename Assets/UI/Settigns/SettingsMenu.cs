using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsMenu : MonoBehaviour
{
    private const string SettingsKey = "Settings";

    [Inject] private ISaveSystem saveSystem;

    [Header("Audio")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    [Header("Buttons")]
    [SerializeField] private Button applyButton;
    [SerializeField] private Button defaultsButton;
    [SerializeField] private Button cancelButton;

    [SerializeField] private GameOptionsData options;

    private async void Start()
    {
        await LoadOptions();
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void UpdateUI()
    {
        sfxSlider.SetValueWithoutNotify(options.sfxVolume);
        musicSlider.SetValueWithoutNotify(options.musicVolume);
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

    #region Event Subscription

    private void SubscribeEvents()
    {
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);

        applyButton.onClick.AddListener(OnApplyClicked);
        defaultsButton.onClick.AddListener(OnDefaultsClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);
    }

    private void UnsubscribeEvents()
    {
        sfxSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
        musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);

        applyButton.onClick.RemoveListener(OnApplyClicked);
        defaultsButton.onClick.RemoveListener(OnDefaultsClicked);
        cancelButton.onClick.RemoveListener(OnCancelClicked);
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
        return new GameOptionsData
        {
            sfxVolume = 1f,
            musicVolume = 1f
        };
    }
}