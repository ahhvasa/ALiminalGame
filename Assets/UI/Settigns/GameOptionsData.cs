using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;


[Serializable]
public class GameOptionsData
{
    [Header("Display")]
    public int screenWidth = 1920;
    public int screenHeight = 1080;
    public bool fullscreen = true;

    [Header("Audio")]
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    [Range(0f, 1f)]
    public float musicVolume = 1f;

    [Header("Localization")]

    public string languageCode = "en";


    public void ApplySettings()
    {
        //ApplyResolution();
        ApplyAudio(SoundManager.Instance.audioMixer);
        //ApplyLanguage();
    }

    private void ApplyResolution()
    {
        Screen.SetResolution(
            screenWidth,
            screenHeight,
            fullscreen
        );
    }

    private void ApplyAudio(AudioMixer audioMixer)
    {
        if (audioMixer == null)
        {
            Debug.LogWarning("AudioMixer not found.");
            return;
        }

        audioMixer.SetFloat("SFXVolume", LinearToDecibel(sfxVolume));
        audioMixer.SetFloat("MusicVolume", LinearToDecibel(musicVolume));
    }

    private void ApplyLanguage()
    {
        if (string.IsNullOrWhiteSpace(languageCode))
            return;

        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.Identifier.Code.Equals(languageCode, StringComparison.OrdinalIgnoreCase))
            {
                LocalizationSettings.SelectedLocale = locale;
                return;
            }
        }

        Debug.LogWarning($"Launguage '{languageCode}' not found.");
    }

    private float LinearToDecibel(float value)
    {
        if (value <= 0.0001f)
            return -80f;

        return Mathf.Log10(value) * 20f;
    }
}