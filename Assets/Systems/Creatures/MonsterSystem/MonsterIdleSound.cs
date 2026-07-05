using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleSound : MonoBehaviour
{
    public SoundPlayer soundPlayer;
    public SoundData idleSound;

    public void Start()
    {
        Sound sound = SoundManager.PlaySound(idleSound, soundPlayer);
    }
}
