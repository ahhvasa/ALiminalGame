using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public void Awake()
    {
        objectPull = new ObjectPull<Sound>(soundObjectPrefab, 8, transform);
        Instance = this;
    }
    public AudioMixer audioMixer;
    public Sound soundObjectPrefab;

    public ObjectPull<Sound> objectPull;

    

    public static Sound PlaySound(ISoundData soundData, SoundPlayer soundPlayer)
    {
        Sound sound = GetSound();
        soundData.ApplyToSound(sound);
        soundPlayer.ClaimSound(sound);

        sound.transform.SetParent(soundPlayer.transform, false);
        sound.transform.localPosition = Vector3.zero;

        sound.Play();

        return sound;
    }

    private static void Return(Sound sound)
    {
        Debug.Log("return");

        Instance.objectPull.ReturnObject(sound);

        sound.transform.SetParent(Instance.transform, false);
        sound.transform.localPosition = Vector3.zero;

        sound.ClearEvent();
    }

    private static Sound GetSound()
    {
        Sound sound = Instance.objectPull.GetObject();
        sound.OnClipEnd += () => Return(sound);
        return sound;
    }
}
