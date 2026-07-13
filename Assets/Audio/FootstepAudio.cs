using UnityEngine;
using System.Collections;

public class FootstepAudio : MonoBehaviour
{
    public SoundData walkSoundData;
    public SoundPlayer soundPlayer;
    private Sound walkSound;

    [SerializeField] private float moveThreshold = 0.05f;
    [SerializeField] private float maxSpeed = 6f;
    [SerializeField] private float minPitch = 0.8f;
    [SerializeField] private float maxPitch = 1.3f;

    private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
        walkSound = SoundManager.PlaySound(walkSoundData, soundPlayer);
        walkSound.Stop();
    }

    public void OnEnable()
    {
        if (SoundManager.Instance == null || walkSound != null) { return; }

        _lastPosition = transform.position;
        walkSound = SoundManager.PlaySound(walkSoundData, soundPlayer);
        walkSound.Stop();
    }

    public void OnDisable()
    {
        if (SoundManager.Instance == null || walkSound == null || gameObject.activeSelf == false) { return; }

        SoundManager.Instance.StartCoroutine(ReturnToPool());
    }
    IEnumerator ReturnToPool()
    {
        yield return null;
        walkSound.DestroySound();
        walkSound = null;
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, _lastPosition);
        float speed = distance / Time.deltaTime;

        _lastPosition = transform.position;

        if (speed > moveThreshold)
        {
            if (!walkSound.IsPlaying)
                walkSound.Play();

            walkSound.audioSource.pitch = Mathf.Lerp(
                minPitch,
                maxPitch,
                Mathf.Clamp01(speed / maxSpeed));
        }
        else
        {
            if (walkSound.IsPlaying)
                walkSound.Stop();
        }
    }
}