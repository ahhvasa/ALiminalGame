using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelMusic : MonoBehaviour
{
    public SoundData music;
    public SoundData scaryAmbience;

    public AudioMixerGroup mixerGroup;
    public SoundPlayer soundPlayer;

    public void Start()
    {
        sound = SoundManager.PlaySound(music, soundPlayer);
    }
    Sound sound;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            sound.StopSmoothly();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            sound.PlaySmoothly();
        }
    }

}
