using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCandel : MonoBehaviour, IPlayerInteractableObject
{
    public SoundData sound;

    public void Interact(Player player)
    {
        if (WorldManager.Instance.isNightOn == false)
        {
            WorldManager.Instance.EnterNight();
            SoundManager.PlaySound(sound, player.soundPlayer);
        }
    }
}
