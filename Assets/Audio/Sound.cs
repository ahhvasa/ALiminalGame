using System;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public ISoundData currentSoundData;

    public event Action OnClipEnd;

    public void ClearEvent()
    {
        OnClipEnd = null;
    }

    private bool wasPlaying;

    void Update()
    {
        if (audioSource.isPlaying)
        {
            wasPlaying = true;
        }
        else if (wasPlaying)
        {
            wasPlaying = false;
            if (currentSoundData.IsLooped() == true)
            {
                Play();
                return;
            }
            End();
        }
    }

    public void Play()
    {
        currentSoundData.ApplyToSound(this);
        audioSource.Play();
    }

    public void End()
    {
        wasPlaying = false;


        OnClipEnd.Invoke();
    }
}
