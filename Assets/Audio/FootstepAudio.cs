using UnityEngine;
using System.Collections;

public class FootstepAudio : MonoBehaviour
{
    public SoundData walkSoundData;
    public SoundData runSoundData;

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

        if (walkSound != null) { return; }
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
        if (SoundManager.Instance == null || walkSound == null || gameObject.activeSelf == false || soundPlayer.isActiveAndEnabled == false) { return; }
        
        SoundManager.Instance.StartCoroutine(ReturnToPool());
    }
    IEnumerator ReturnToPool()
    {
        yield return null;

        if (walkSound != null) 
        {

            walkSound?.DestroySound();
            walkSound = null;
        }

    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, _lastPosition);
        float speed = distance / Time.deltaTime;

        _lastPosition = transform.position;

        Debug.Log("my speed = " + speed + " walkSound = " + walkSound);

        if (speed > moveThreshold)
        {
            if (!walkSound.IsPlaying)
                walkSound.Play();

            walkSound.audioSource.pitch = Mathf.Lerp(
                minPitch,
                maxPitch,
                Mathf.Clamp01(speed / maxSpeed));


            walkSound.audioSource.volume = Mathf.Lerp(
                0,
                1,
                Mathf.Clamp01(speed / maxSpeed));
        }
        else
        {
            //if (walkSound.IsPlaying)
            //    walkSound.Stop();

            walkSound.audioSource.volume = 0;
        }
    }
}