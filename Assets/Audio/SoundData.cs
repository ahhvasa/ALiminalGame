using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundData : ISoundData
{
    public AudioMixerGroup mixerGroup;
    public AudioClip clip;
    public bool looped;

    public SoundData(AudioMixerGroup mixerGroup, AudioClip clip)
    {
        this.mixerGroup = mixerGroup;
        this.clip = clip;
    }

    public void ApplyToSound(Sound sound)
    {
        sound.currentSoundData = this;

        sound.audioSource.clip = clip;
        sound.audioSource.outputAudioMixerGroup = mixerGroup;
        // sound.audioSource.loop = looped;
    }

    public bool IsLooped()
    {
        return looped;
    }
}

public interface ISoundData
{
    public void ApplyToSound(Sound sound);
    public bool IsLooped();
}