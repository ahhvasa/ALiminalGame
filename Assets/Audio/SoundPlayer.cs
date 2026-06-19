using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public List<Sound> playingSounds;

    public void ClaimSound(Sound sound)
    {
        playingSounds.Add(sound);
        sound.OnClipEnd += () => { playingSounds.Remove(sound); };
    }
}
