using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomManadger : MonoBehaviour
{
    public static RoomManadger Instance;
    public List<Room> allRooms;

    public static List<Room> AllRooms { get { return Instance.allRooms; } }

    public void Awake()
    {
        Instance = this;
        allRooms = GameObject.FindObjectsOfType<Room>().ToList();
    }

    public static Room GetClosestRoom(Vector3 position)
    {
        Room closestRoom = null;
        float closestDistance = float.MaxValue;

        foreach (Room room in RoomManadger.Instance.allRooms)
        {
            float distance = Vector3.Distance(position, room.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestRoom = room;
            }
        }

        return closestRoom;
    }

    public static List<T> GetComponentInRooms<T>(List<Room> rooms) 
        where T : Object
    {
        List<T> objects = new List<T>();
        foreach (Room room in rooms)
        {
            objects.AddRange(room.GetComponentsInChildren<T>());
        }
        return objects;
    }
}
