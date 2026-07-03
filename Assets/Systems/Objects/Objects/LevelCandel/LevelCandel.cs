using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCandel : MonoBehaviour, IPlayerInteractableObject
{
    public void Interact(Player player)
    {
        WorldManadger.Instance.EnterNight();
    }
}
