using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelMusic : MonoBehaviour
{
    public SoundData dayMusic;
    public SoundData nightMusic;
    public SoundData scaryAmbience;

    public AudioMixerGroup mixerGroup;
    public SoundPlayer soundPlayer;

    Sound currentMusic;
    Sound currentSFX;

    public void Start()
    {
        currentMusic = SoundManager.PlaySound(dayMusic, soundPlayer);
        currentMusic.Stop();

        currentSFX = SoundManager.PlaySound(scaryAmbience, soundPlayer);
        currentSFX.Stop();


        WorldManager.Instance.OnDayStart += () => { PlayDayMusic(); };
        WorldManager.Instance.OnNightStart += () => { PlayNightMusic(); };

        if (WorldManager.Instance.stateMachine.Current is WorldDayState) { PlayDayMusic(); }
        if (WorldManager.Instance.stateMachine.Current is WorldNightState) { PlayNightMusic(); }
    }
    
    public void Update()
    {

    }

    public async UniTask PlayDayMusic()
    {
        currentSFX.StopWaitAndPlay();

        await currentMusic.PlaySmoothly(dayMusic);
    }

    public async UniTask PlayNightMusic()
    {
        currentSFX.StartWaitAndPlay();

        await currentMusic.PlaySmoothly(nightMusic);
    }

    public async UniTask PlayScarySound()
    {
        await currentSFX.PlaySmoothly(scaryAmbience);
    }

    public async void StopMusic()
    {
        currentSFX.StopSmoothly();
        await currentMusic.StopSmoothly();
    }

}
