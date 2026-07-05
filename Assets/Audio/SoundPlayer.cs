using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public List<Sound> playingSounds;

    public void ClaimSound(Sound sound)
    {
        playingSounds.Add(sound);
        sound.OnSoundDestroy += () => { playingSounds.Remove(sound); };
    }
}
