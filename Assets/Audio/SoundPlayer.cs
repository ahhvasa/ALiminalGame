using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public PerceivableObject perceivableObject;

    public void Awake()
    {
        if (perceivableObject == null)
        {
            if (gameObject.TryGetComponent<PerceivableObject>(out perceivableObject) == false)
            {
                perceivableObject = gameObject.AddComponent<PerceivableObject>();
            }
        }
        perceivableObject.soundPlayer = this;
    }

    public List<Sound> playingSounds;

    public void ClaimSound(Sound sound)
    {
        playingSounds.Add(sound);
        sound.OnSoundDestroy += () => { playingSounds.Remove(sound); sound.currentSoundPlayer = null; };
        sound.currentSoundPlayer = this;
    }
}
