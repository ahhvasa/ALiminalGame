using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Loading;
using UnityEngine;
using Zenject;

public class RoomBuilder : MonoBehaviour
{
    [Inject] PrefabFactory prefabFactory;

    public string emptyRoomMarkAddressiblesKey;

    public Room room;

    public void Start()
    {
        BuildRoom();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            BuildRoom();
        }
    }

    public async void BuildRoom()
    {
        await InitializeRoomPartMarks();

        RoomPartMark[] marks = FindObjectsOfType<RoomPartMark>();
        foreach (RoomPartMark mark in marks)
        {
            var task = prefabFactory.LoadPrefabAsync(mark.addressiblesKey);
            await task;
            GameObject gameObject = task.Result;
            RoomPart roomPart = gameObject.GetComponent<RoomPart>();
            roomPart.transform.position = mark.transform.position;

            foreach (var connectedPart in mark.roomConnectedParts)
            {
                connectedPart.roomPart = roomPart;
                roomPart.rooms.Add(connectedPart.hostRoom);
                roomPart.transform.LookAt(connectedPart.hostRoom.transform.position);
            }
        }
    }


    public async Task InitializeRoomPartMarks()
    {
        List<RoomPartMark> roomPartMarks = FindObjectsOfType<RoomPartMark>().ToList();
        List<Room> rooms = RoomManadger.AllRooms;
        foreach (var roomPartMark in roomPartMarks)
        {
            Room nearestRoom = rooms
                .Where(room => Vector3.Distance(room.transform.position, roomPartMark.transform.position) < squareSize)
                .OrderBy(room => (room.transform.position - roomPartMark.transform.position).sqrMagnitude)
                .FirstOrDefault();
            if (nearestRoom == null) { continue; }

            RoomConnectedPart nearestRoomConnectedPart = 
                nearestRoom.GetRoomConnectedParts()
                .OrderBy(part => (part.transform.position - roomPartMark.transform.position).sqrMagnitude)
                .FirstOrDefault();

            roomPartMark.transform.position = nearestRoomConnectedPart.transform.position;
        }

        RoomConnectedPart[] roomConnectedParts = FindObjectsOfType<RoomConnectedPart>();
        foreach (var roomConnectedPart in roomConnectedParts)
        {
            List<RoomPartMark> marks = SceneSearchService.FindAllObjectsInSquareZone<RoomPartMark>(roomConnectedPart.transform.position, 1);

            if (marks.Count == 0)
            {
                var task = prefabFactory.LoadPrefabAsync(emptyRoomMarkAddressiblesKey);
                await task;
                var mark = task.Result.GetComponent<RoomPartMark>();
                mark.transform.position = roomConnectedPart.transform.position;
                marks.Add(mark);
            }


            for (int i = 0; i != marks.Count; i++)
            {
                if (i == 0)
                {
                    marks[i].roomConnectedParts.Add(roomConnectedPart);
                }
                else
                {
                    marks[i].transform.position = marks[i].transform.position + Vector3.up * -20;
                    Debug.LogWarning("Extra room part mark detected");
                }
            }
        }
    }



    [SerializeField] private float squareSize = 10.5f;




}
