using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSound : MonoBehaviour
{
    public SoundPlayer soundPlayer;
    public SoundData idleSound;
    public SoundData surpriseSound;

    public void Start()
    {
        Sound sound = SoundManager.PlaySound(idleSound, soundPlayer);
    }

    public void SurpriseSound()
    {
        Sound sound = SoundManager.PlaySound(surpriseSound, soundPlayer);
    }
}