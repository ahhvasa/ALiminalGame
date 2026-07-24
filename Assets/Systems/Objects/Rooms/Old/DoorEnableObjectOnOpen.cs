using UnityEngine;

public class DoorEnableObjectOnOpen : MonoBehaviour
{
    public RoomDoor roomDoor;

    public GameObject[] gameObjects;

    public void Awake()
    {
        roomDoor.OnOpen += SetActiveToObjects;
        SetActiveToObjects(false);
    }

    public void SetActiveToObjects(bool setActive)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(setActive);
        }
    }
}