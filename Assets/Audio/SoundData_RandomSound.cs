using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundData_RandomSound: ISoundData
{
    public AudioMixerGroup mixerGroup;
    public List<AudioClip> clips;
    public bool looped;

    public SoundData_RandomSound(AudioMixerGroup mixerGroup, List<AudioClip> clips)
    {
        this.mixerGroup = mixerGroup;
        this.clips = clips;
    }

    public void ApplyToSound(Sound sound)
    {
        Debug.Log("Random!");

        sound.currentSoundData = this;

        sound.audioSource.clip = clips[UnityEngine.Random.Range(0, clips.Count)];
        sound.audioSource.outputAudioMixerGroup = mixerGroup;
        //sound.audioSource.loop = looped;
    }

    public bool IsLooped()
    {
        return looped;
    }
}
