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
            item.transform.position = new Vector3(999, -999, 999);

            if (boxCount >= needToWin)
            {
                Win();
            }
        }
    }

    public void Win()
    {
        boxCount = 0;
        WorldManager.Instance.EnterDay();
        WorldManager.Instance.Win();
    }
}
