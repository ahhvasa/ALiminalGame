using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSound : MonoBehaviour
{
    public SoundPlayer soundPlayer;
    public SoundData idleSound;
    public SoundData surpriseSound;
    public SoundData eatObjectSound;

    public void Start()
    {
        Sound sound = SoundManager.PlaySound(idleSound, soundPlayer);
    }

    public void SurpriseSound()
    {
        Sound sound = SoundManager.PlaySound(surpriseSound, soundPlayer);
    }

    public void EatObjectSound()
    {
        Sound sound = SoundManager.PlaySound(eatObjectSound, soundPlayer);
    }
}