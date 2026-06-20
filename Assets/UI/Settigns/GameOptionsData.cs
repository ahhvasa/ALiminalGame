using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;


[Serializable]
public class GameOptionsData
{
    [Header("Display")]
    public int resolutionWidth;
    public int resolutionHeight;
    public bool fullscreen = true;
    public Resolution resolution
    {
        get
        {
            Resolution res = new Resolution();
            res.width = resolutionWidth;
            res.height = resolutionHeight;
            return res;
        }
        set
        {
            resolutionWidth = value.width;
            resolutionHeight = value.height;
        }
    }

    [Header("Audio")]
    [Range(0f, 1f)]
    public float sfxVolume = 1f;
    [Range(0f, 1f)]
    public float musicVolume = 1f;

    [Header("Localization")]
    public string languageCode;
    public Locale language
    {
        get
        {
            return LocalizationSettings.AvailableLocales.Locales
            .FirstOrDefault(l => l.Identifier.Code == languageCode);
        }
        set
        {
            languageCode = value.Identifier.Code;
        }
    }

    public static GameOptionsData Create(Resolution currentResolution, float sfxVolume, float musicVolume, Locale language)
    {
        GameOptionsData data = new GameOptionsData();
        data.resolution = currentResolution;
        data.sfxVolume = sfxVolume;
        data.musicVolume = musicVolume;
        data.language = language;
        return data;
    }

    public void ApplySettings()
    {
        ApplyAudio(SoundManager.Instance.audioMixer);
        ApplyResolution();
        ApplyLanguage();
    }

    private void ApplyResolution()
    {
        Screen.SetResolution(
            resolutionWidth,
            resolutionHeight,
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
        LocalizationSettings.SelectedLocale = language;
    }

    private float LinearToDecibel(float value)
    {
        if (value <= 0.0001f)
            return -80f;

        return Mathf.Log10(value) * 20f;
    }
}