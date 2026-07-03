using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour, IPlayerInteractableObject
{
    public int boxCount = 0;
    public int needToWin = 3;

    public void Interact(Player player)
    {
        if (player.playerInventory.CurrentItem != null)
        {
            boxCount += 1;
            var item = player.playerInventory.TakeItem();
            item.SetExistence(false);

            if (boxCount >= needToWin)
            {
                Win();
            }
        }
    }

    public void Win()
    {
        WorldManadger.Instance.EnterDay();
        WorldManadger.Instance.Win();
    }
}
