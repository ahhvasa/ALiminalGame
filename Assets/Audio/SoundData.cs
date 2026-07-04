using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundData: ISoundData
{
    public AudioMixerGroup mixerGroup;
    public List<AudioClip> clips;
    public SoundLoopPlay loopPlayInterval;
    public bool removeDistantSoundReduction;

    public SoundData(AudioMixerGroup mixerGroup, List<AudioClip> clips)
    {
        this.mixerGroup = mixerGroup;
        this.clips = clips;
    }

    public void ApplyToSound(Sound sound)
    {
        sound.currentSoundData = this;

        if (clips.Count == 0) { return; }

        sound.audioSource.clip = clips[UnityEngine.Random.Range(0, clips.Count)];
        sound.audioSource.outputAudioMixerGroup = mixerGroup;
    }

    public bool IsLooped()
    {
        return loopPlayInterval.looped;
    }

    public float GetLoopInterval()
    {
        return loopPlayInterval.GetInterval();
    }

    public bool UseDistantSoundReduction { get { return !removeDistantSoundReduction; } }

}

[Serializable]
public class SoundLoopPlay
{
    public bool looped;
    public float minimumInterval;
    public float maximumInterval;

    public float GetInterval()
    {
        return UnityEngine.Random.Range(minimumInterval, maximumInterval);
    }
}