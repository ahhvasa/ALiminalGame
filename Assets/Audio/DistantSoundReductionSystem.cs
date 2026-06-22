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
        VisibleObject soundParent;
        try
        {
            soundParent = sound.GetComponentInParent<VisibleObject>();

            var mesh = soundParent.GetComponent<MeshRenderer>();

            if (mesh.material.color.a < 0.5f)
            {
                sound.audioLowPassFilter.enabled = true;
            }
            else
            {
                sound.audioLowPassFilter.enabled = false;
            }
        }
        catch
        {
            return;
        }
    }
}
