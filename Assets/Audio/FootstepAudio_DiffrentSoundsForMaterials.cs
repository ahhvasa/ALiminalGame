using UnityEngine;
using System.Collections;

public class FootstepAudio_DiffrentSoundsForMaterials : MonoBehaviour
{
    public SoundData walkSound_Default;

    public SoundPlayer soundPlayer;
    private Sound walkSound;

    public float moveThreshold = 0.05f;
    public float maxSpeed = 6f;
    public float minPitch = 0.8f;
    public float maxPitch = 1.3f;
    public float maxSoundDistance = 10f;
    public float maxVolume = 1;

    private Vector3 _lastPosition;

    public bool blockSound = false;

    private void Start()
    {
        _lastPosition = transform.position;

        if (walkSound != null) { return; }
        walkSound = SoundManager.PlaySound(walkSound_Default, soundPlayer);
        walkSound.Stop();
    }

    private void Awake()
    {
        ProcessSoundData(walkSound_Tile);
        ProcessSoundData(walkSound_Linoleum);
        ProcessSoundData(walkSound_Parquet);
        ProcessSoundData(walkSound_Pavement);
        ProcessSoundData(walkSound_Plastic);
        ProcessSoundData(walkSound_Rubber);
        ProcessSoundData(walkSound_Grass);
        ProcessSoundData(walkSound_Metal_Flat);
        ProcessSoundData(walkSound_Metal_Noisy);
        ProcessSoundData(walkSound_Metal_Low);

        void ProcessSoundData(SoundData soundData)
        {
            soundData.removeDistantSoundReduction = true;
            soundData.loopPlayInterval.looped = true;

            soundData.aIPerceivedSoundData.isAiPerceived = true;
            soundData.aIPerceivedSoundData.soundDistance = 10;
            soundData.aIPerceivedSoundData.soundType = AiPerceivedSoundType.step;
        }
    }

    public void OnEnable()
    {
        if (SoundManager.Instance == null || walkSound != null) { return; }

        _lastPosition = transform.position;
        walkSound = SoundManager.PlaySound(walkSound_Default, soundPlayer);
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

    string currentGroundTag;

    private void FixedUpdate()
    {
        string groundTag = CheckGroundTag();
        if (currentGroundTag != groundTag)
        {
            currentGroundTag = groundTag;
            walkSound.DestroySound();
            walkSound = null;

            walkSound = SoundManager.PlaySound(GetSoundByTag(currentGroundTag), soundPlayer);
            walkSound.Stop();
        }



        float distance = Vector3.Distance(transform.position, _lastPosition);
        float speed = distance / Time.deltaTime;

        _lastPosition = transform.position;


        if (speed > moveThreshold)
        {
            if (!walkSound.IsPlaying)
                walkSound.Play();

            float t = Mathf.Clamp01(speed / maxSpeed);

            walkSound.audioSource.pitch = Mathf.Lerp(
                minPitch,
                maxPitch,
                t);


            walkSound.audioSource.volume = Mathf.Lerp(
                0,
                maxVolume,
                t);

            if (blockSound)
            {
                walkSound.aIPerceivedSoundData.soundDistance = 0;
            }
            else
            {

                walkSound.aIPerceivedSoundData.soundDistance = Mathf.Lerp(
                    0,
                    maxSoundDistance,
                    t);
            }

        }
        else
        {
            if (walkSound.IsPlaying)
                walkSound.Stop();

            walkSound.audioSource.volume = 0;
        }
    }

    public LayerMask layerMask;
    public string defaultTag = "Default";

    public string CheckGroundTag()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            return hit.collider.gameObject.tag;
        }

        return defaultTag;
    }

    public SoundData walkSound_Tile;
    public SoundData walkSound_Linoleum;
    public SoundData walkSound_Parquet;
    public SoundData walkSound_Pavement;
    public SoundData walkSound_Plastic;
    public SoundData walkSound_Rubber;
    public SoundData walkSound_Grass;
    public SoundData walkSound_Metal_Flat;
    public SoundData walkSound_Metal_Noisy;
    public SoundData walkSound_Metal_Low;


    public ISoundData GetSoundByTag(string tag)
    {
        switch (tag)
        {
            case "Tile":
                return walkSound_Tile;

            case "Linoleum":
                return walkSound_Linoleum;

            case "Parquet":
                return walkSound_Parquet;

            case "Pavement":
                return walkSound_Pavement;

            case "Plastic":
                return walkSound_Plastic;

            case "Rubber":
                return walkSound_Rubber;

            case "Grass":
                return walkSound_Grass;

            case "Metal_Flat":
                return walkSound_Metal_Flat;

            case "Metal_Noisy":
                return walkSound_Metal_Noisy;

            case "Metal_Low":
                return walkSound_Metal_Low;

            default:
                return walkSound_Default;
        }
    }

}