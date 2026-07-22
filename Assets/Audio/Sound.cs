using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Cysharp.Threading.Tasks;


public class Sound : MonoBehaviour, IPercivableObject
{
    public SoundPlayer currentSoundPlayer;
    public PerceivableObject PerceivableObject
    {
        get
        {
            return currentSoundPlayer?.perceivableObject;
        }
    }

    public AudioSource audioSource;
    public ISoundData currentSoundData;

    public AIPerceivedSoundData aIPerceivedSoundData;

    [Header("Filters")]
    public AudioLowPassFilter audioLowPassFilter;

    public bool IsPlaying { get { return isPlaying; } }
    private bool isPlaying;

    public event Action OnSoundDestroy;

    public void ClearEvent()
    {
        OnSoundDestroy = null;
    }


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
            if (Mathf.Approximately(intervalTime, 0))
            {
                Play();
            }
            else
            {
                StartWaitAndPlay();
            }
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
        if (IsPlaying) { return; }
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


    public async UniTask StopSmoothly()
    {
        await SmoothlyChangeVolumeAsync(1f, 0f);
        Stop();
    }
    public async UniTask PlaySmoothly()
    {
        Play();
        if (audioSource.volume < 1f)
        {
            await SmoothlyChangeVolumeAsync(audioSource.volume, 1f);
        }
    }
    public async UniTask PlaySmoothly(ISoundData newSoundData)
    {
        if (currentSoundData != newSoundData)
        {
            await StopSmoothly();
            currentSoundData = newSoundData;
        }
        await PlaySmoothly();
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


    public void StartWaitAndPlay()
    {
        intervalTime = currentSoundData.GetLoopInterval();
        waitAndPlayCoroutine = StartCoroutine(WaitAndPlay());
    }
    public void StopWaitAndPlay()
    {
        if (waitAndPlayCoroutine != null)
        {
            StopCoroutine(waitAndPlayCoroutine);
            waitAndPlayCoroutine = null;
        }
    }
    private IEnumerator WaitAndPlay()
    {
        yield return new WaitForSeconds(intervalTime);
        waitAndPlayCoroutine = null;
        Play();
    }

    private void OnDisable()
    {
        StopWaitAndPlay();
        audioSource.volume = targetVolume;
    }




}

[Serializable]
public class AIPerceivedSoundData
{
    public bool isAiPerceived;
    public AiPerceivedSoundType soundType;
    public float soundDistance = 10;
}

public enum AiPerceivedSoundType
{
    step,
    monsterScream,
    ring,
    itemSound,
    meatExplosion
}