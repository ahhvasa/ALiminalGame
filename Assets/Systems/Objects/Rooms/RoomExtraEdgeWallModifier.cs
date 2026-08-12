using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class RoomExtraEdgeWallModifier : MonoBehaviour
{
    public Room room;
    public VisibleObject visibleObject;
    public string prefabKey;
    public string roomName;

    public void BuildWalls()
    {
        foreach (var part in room.roomParts)
        {
            if (part.roomPart.TryGetConnectedRoom(room, out Room connectedRoom))
            {
                if (part.roomPart is EmptyRoomPart)
                {
                    if (connectedRoom.TryGetComponent<RoomExtraEdgeWallModifier>(out var component))
                    {
                        if (component.roomName != roomName)
                        {
                            CreateExtraWall(part.roomPart.transform);
                        }
                    }
                }
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