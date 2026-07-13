using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using Unity.Loading;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

[ExecuteAlways]
public class RoomBuilder : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public string emptyRoomMarkAddressiblesKey;
    public string roomConnectionPointAddressiblesKey;

    public void ClearBuilding()
    {
        AssembledByRoomBuilderFlag[] objects = FindObjectsOfType<AssembledByRoomBuilderFlag>();
        foreach (var obj in objects)
        {
            GameObject.DestroyImmediate(obj.gameObject);
        }
        navMeshSurface.RemoveData();
        EditorUtility.SetDirty(navMeshSurface);
    }

    public async Task Build()
    {
        ClearBuilding();

        List<RoomConnectionPoint> connectionPoints = await CreateRoomConnectionPoints();

        foreach (RoomConnectionPoint point in connectionPoints)
        {
            var task = LoadPrefabAsync(point.partMark.addressiblesKey);
            await task;
            GameObject gameObject = task.Result;
            RoomPart roomPart = gameObject.GetComponent<RoomPart>();
            Debug.Log($"roomPart = {roomPart}");
            roomPart.AddComponent<AssembledByRoomBuilderFlag>();
            roomPart.transform.position = point.transform.position;
            EditorUtility.SetDirty(roomPart);

            foreach (RoomConnectedPart connectedPart in point.roomConnectedParts)
            {
                Undo.RecordObject(connectedPart, "Assign RoomPart");
                connectedPart.roomPart = roomPart;
                EditorUtility.SetDirty(connectedPart);

                roomPart.rooms.Add(connectedPart.hostRoom);
                roomPart.transform.LookAt(connectedPart.hostRoom.transform.position);
                EditorUtility.SetDirty(roomPart);
            }
        }

        ApplyTextures();
        navMeshSurface.BuildNavMesh();
    }

    public void ApplyTextures()
    {
        Room[] rooms = FindObjectsOfType<Room>();
        foreach (var room in rooms)
        {
            room.ApplyTextures();
            EditorUtility.SetDirty(room);
        }
    }

    public async Task<List<RoomConnectionPoint>> CreateRoomConnectionPoints()
    {
        RoomConnectedPart[] roomConnections = GameObject.FindObjectsOfType<RoomConnectedPart>();
        List<RoomConnectionPoint> connectionPoint = new List<RoomConnectionPoint>();

        RoomPartMark defaultPartMark = (await LoadPrefabAsync(emptyRoomMarkAddressiblesKey)).GetComponent<RoomPartMark>();
        defaultPartMark.transform.position = new Vector3(0, -999, 0);
        defaultPartMark.AddComponent<AssembledByRoomBuilderFlag>();
        EditorUtility.SetDirty(defaultPartMark);

        foreach (var roomConnection in roomConnections)
        {
            await CreatePoint(roomConnection);
        }

        async Task CreatePoint(RoomConnectedPart roomConnectionPart)
        {
            float maxDistance = 1f;
            if (SceneSearchService.TryFindNearest(roomConnectionPart.transform.position, maxDistance, out RoomConnectionPoint existingConnectionPoint))
            {
                existingConnectionPoint.roomConnectedParts.Add(roomConnectionPart);
                EditorUtility.SetDirty(existingConnectionPoint);
                return;
            }
            var task = LoadPrefabAsync(roomConnectionPointAddressiblesKey);
            await task;
            RoomConnectionPoint connecionPoint = task.Result.GetComponent<RoomConnectionPoint>();
            connectionPoint.Add(connecionPoint);

            connecionPoint.roomConnectedParts.Add(roomConnectionPart);
            connecionPoint.transform.position = roomConnectionPart.transform.position;
            connecionPoint.Initialize(defaultPartMark);
            connecionPoint.AddComponent<AssembledByRoomBuilderFlag>();
            EditorUtility.SetDirty(connecionPoint);
        }
        return connectionPoint;
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
