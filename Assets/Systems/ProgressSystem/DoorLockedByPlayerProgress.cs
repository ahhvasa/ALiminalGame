using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class DoorLockedByPlayerProgress : MonoBehaviour
{
    public List<DoorAndPlayerProgressPair> doors;

    void Start()
    {
        OpenDoors();
    }

    async Task OpenDoors()
    {
        PlayerProgress playerProgress = await PlayerPrefsSaveSystem_StaticSingleton.Instance.LoadAsync<PlayerProgress>(PlayerProgress.fileName);

        foreach (var pair in doors)
        {
            if (playerProgress == null)
            {
                pair.door.active = false;
            }

            if (playerProgress.finishedLevels.Contains(pair.levelReqiered))
            {
                pair.door.active = true;
            }
            else
            {
                pair.door.active = false;
            }
        }
    }
}



[Serializable]
public class DoorAndPlayerProgressPair
{
    public RoomDoor door;
    public string levelReqiered;
}

[Serializable]
public class PlayerProgress
{
    public const string fileName = "playerProgress";
    public List<string> finishedLevels = new List<string>();
}