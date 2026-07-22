using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CCTVCameraManager : MonoBehaviour
{
    public static CCTVCameraManager Instance;
    public List<Item_CCTVCamera> cameras;

    public void Start()
    {
        Instance = this;
        cameras = GameObject.FindObjectsOfType<Item_CCTVCamera>().ToList();
    }

    public static List<Room> GetRoomsSeenByCameras()
    {
        List<Room> rooms = new List<Room>();
        foreach (var camera in Instance.cameras)
        {
            if (camera.IsInInventory || camera.Working == false) { continue; }

            rooms.Add(RoomManadger.GetClosestRoom(camera.itemObject.transform.position));
        }
        return rooms;
    }

}

