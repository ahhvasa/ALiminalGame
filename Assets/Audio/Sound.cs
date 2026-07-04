using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;


public class Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public ISoundData currentSoundData;

    [Header("Filters")]
    public AudioLowPassFilter audioLowPassFilter;

    public event Action OnSoundDestroy;

    public void ClearEvent()
    {
        OnSoundDestroy = null;
    }

    private bool isPlaying;

    void Update()
    {
        if (audioSource.isPlaying)
        {
            isPlaying = true;
        }
        else if (audioSource.isPlaying == false && isPlaying)
        {
            isPlaying = false;
            OnFinishClip();
        }
    }

    void OnFinishClip()
    {
        if (currentSoundData.IsLooped() == true)
        {
            StartWaitAndPlay(currentSoundData.GetLoopInterval());
        }
        else
        {
            DestroySound();
        }
    }

    public void Play(ISoundData newSoundData)
    {
        currentSoundData = newSoundData;
        Play();
    }

    public void Play()
    {
        currentSoundData.ApplyToSound(this);
        audioSource.Play();
    }

    public void DestroySound()
    {
        isPlaying = false;
        OnSoundDestroy.Invoke();
    }

    public void Stop()
    {
        isPlaying = false;
        audioSource.Stop();
    }


    public async void StopSmoothly()
    {
        await SmoothlyChangeVolumeAsync(1f, 0f);
        Stop();
    }
    public async void PlaySmoothly()
    {
        Play();
        if (audioSource.volume < 1f)
        {
            await SmoothlyChangeVolumeAsync(audioSource.volume, 1f);
        }
    }





    [SerializeField] private float volumeSmoothChangeDuration = 1f;
    private float targetVolume = 1;

    private async UniTask SmoothlyChangeVolumeAsync(float from, float to)
    {
        targetVolume = to;
        audioSource.volume = from;

        float time = 0f;

        while (time < volumeSmoothChangeDuration)
        {
            if (!isActiveAndEnabled)
            {
                audioSource.volume = targetVolume;
                return;
            }

            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(from, to, time / volumeSmoothChangeDuration);

            await UniTask.Yield(PlayerLoopTiming.Update);
        }

        audioSource.volume = targetVolume;
    }


    private Coroutine waitAndPlayCoroutine;
    public float intervalTime = 1;


    private void StartWaitAndPlay(float time)
    {
        intervalTime = time;
        waitAndPlayCoroutine = StartCoroutine(WaitAndPlay());
    }
    private IEnumerator WaitAndPlay()
    {
        yield return new WaitForSeconds(intervalTime);
        waitAndPlayCoroutine = null;
        Play();
    }

    private void OnDisable()
    {
        if (waitAndPlayCoroutine != null)
        {
            StopCoroutine(waitAndPlayCoroutine);
            waitAndPlayCoroutine = null;
        }
        audioSource.volume = targetVolume;
    }




}
