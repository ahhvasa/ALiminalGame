using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelMusic : MonoBehaviour
{
    public SoundData_RandomSound music;
    public SoundData_RandomSound scaryAmbience;

    public AudioMixerGroup mixerGroup;
    public SoundPlayer soundPlayer;

    public void Start()
    {
        SoundManager.PlaySound(music, soundPlayer);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SoundManager.PlaySound(scaryAmbience, soundPlayer);
        }
    }
}
