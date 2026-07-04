using System;
using UnityEngine;
using UnityEngine.Audio;

public interface ISoundData
{
    public void ApplyToSound(Sound sound);
    public bool IsLooped();
    public float GetLoopInterval();

    bool UseDistantSoundReduction { get; }
}