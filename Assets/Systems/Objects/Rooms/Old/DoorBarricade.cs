using UnityEngine;

public abstract class DoorBarricade : MonoBehaviour
{
    public Item_Planks item;
    public RoomDoor roomDoor;

    public abstract void Install(RoomDoor roomDoor);
    public abstract void Break();
    public float doorBarricadeDurability = 1;

    public abstract void StartBreaking();
    public abstract void CancelBreaking();

    public float GetBreakingTime(float breakingPower)
    {
        return doorBarricadeDurability / breakingPower;
    }
}
