using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RoomExtraWallModificator : MonoBehaviour
{
    public Room room;
    public VisibleObject visibleObject;
    public string prefabKey;

    public void BuildWalls()
    {
        foreach (var part in room.roomParts)
        {
            if (part.roomPart.TryGetConnectedRoom(room, out Room connectedRoom))
            {
                if (connectedRoom.GetComponent<RoomExtraWallModificator>() == null)
                {
                    CreateExtraWall(part.roomPart.transform);
                }
                else
                {
                    if (part.roomPart is EmptyRoomPart == false)
                    {
                        CreateExtraWall(part.roomPart.transform);
                    }
                }
            }
            else
            {
                CreateExtraWall(part.roomPart.transform);
            }
        }
    }

    public async void CreateExtraWall(Transform initialWall)
    {
        GameObject wall = await LoadPrefabAsync(prefabKey);
        wall.transform.position = initialWall.position;
        wall.transform.LookAt(room.roomCenter.transform.position);
        wall.transform.parent = room.transform;
    }

    public async Task<GameObject> LoadPrefabAsync(string key)
    {
        AsyncOperationHandle<GameObject> handle =
            Addressables.LoadAssetAsync<GameObject>(key);

        GameObject prefab = await handle.Task;

        EditorUtility.SetDirty(prefab);
        return GameObject.Instantiate(prefab);
    }
}
