using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistantSoundReductionSystem : MonoBehaviour
{
    public void FixedUpdate()
    {
        foreach (var sound in SoundManager.Instance.currentActiveSounds)
        {
            Process(sound);
        }

    }
    public void Process(Sound sound)
    {
        if (sound.currentSoundData.RemoveDistantSoundReduction) { return; }

        VisibleObject soundParent = sound.GetComponentInParent<VisibleObject>();
        if (soundParent == null) { return; }

        float progress = soundParent.CurrentProgress;

        sound.audioSource.volume = Mathf.Lerp(0.1f, 1, progress);

        if (progress < 0.5f)
        {
            sound.audioLowPassFilter.enabled = true;
        }
        else
        {
            sound.audioLowPassFilter.enabled = false;
        }
    }
}
