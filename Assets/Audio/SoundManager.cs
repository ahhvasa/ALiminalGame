using System;
using System.Collections.Generic;
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
    public List<Sound> currentActiveSounds = new List<Sound>();

    public ItemSounds itemSounds;

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
        Instance.objectPull.ReturnObject(sound);

        sound.transform.SetParent(Instance.transform, false);
        sound.transform.localPosition = Vector3.zero;

        sound.ClearEvent();

        Instance.currentActiveSounds.Remove(sound);
    }

    private static Sound GetSound()
    {
        Sound sound = Instance.objectPull.GetObject();
        sound.OnSoundDestroy += () => Return(sound);
        Instance.currentActiveSounds.Add(sound);
        return sound;
    }
}


[Serializable]
public class ItemSounds
{
    public SoundData bulkyPickUp;
    public SoundData cardboardPickUp;
    public SoundData standartPickUp;
    public SoundData plasticPickUp;
    public SoundData ragPickUp;
    public SoundData woodPickUp;

    public SoundData GetPickUpSound(itemTextureType textureType)
    {
        return textureType switch
        {
            itemTextureType.bulky => bulkyPickUp,
            itemTextureType.cardboard => cardboardPickUp,
            itemTextureType.standart => standartPickUp,
            itemTextureType.plastic => plasticPickUp,
            itemTextureType.rag => ragPickUp,
            itemTextureType.wood => woodPickUp,
            _ => standartPickUp
        };
    }

    public SoundData itemDropDown;

    public SoundData GetDropDownSound()
    {
        return itemDropDown;
    }
}
