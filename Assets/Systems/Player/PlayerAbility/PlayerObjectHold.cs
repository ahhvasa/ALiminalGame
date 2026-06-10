using UnityEngine;

public class PlayerObjectHold : MonoBehaviour
{
    public Player player;
    public Vector3 offset;

    Transform currentTargrt;
    public void HoldObject(Transform target)
    {
        DropObject();
        currentTargrt = target;
        target.SetParent(player.transform);
        target.localPosition = Vector3.zero + offset;
    }
    public void DropObject()
    {
        currentTargrt?.SetParent(null);
        currentTargrt = null;
    }
}