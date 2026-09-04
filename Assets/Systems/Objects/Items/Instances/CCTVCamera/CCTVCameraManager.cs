using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CCTVCameraManager : MonoBehaviour
{
    public static CCTVCameraManager Instance;
    public List<Item_CCTVCamera> allCameras = new List<Item_CCTVCamera>();
    public List<Item_CCTVCamera> activeCameras = new List<Item_CCTVCamera>();

    public SoundData onChooseCamera;

    public void Start()
    {
        Instance = this;
        allCameras = GameObject.FindObjectsOfType<Item_CCTVCamera>().ToList();
    }

    public static List<Room> GetRoomsSeenByCameras()
    {
        List<Room> rooms = new List<Room>();
        foreach (var camera in Instance.activeCameras)
        {
            if (camera.IsInInventory) { continue; }

            rooms.Add(RoomManager.GetClosestRoom(camera.itemObject.transform.position));
        }
        return rooms;
    }

}

