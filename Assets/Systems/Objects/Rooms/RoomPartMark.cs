using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomPartMark : MonoBehaviour
{
    public string addressiblesKey;
    public Vector3 position;

    public List<RoomConnectedPart> roomConnectedParts = new();
}
