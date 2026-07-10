using System.Collections.Generic;
using UnityEngine;

public class RoomConnectionPoint : MonoBehaviour
{
    public List<RoomConnectedPart> roomConnectedParts = new();
    public RoomPartMark partMark;
    float maxDistance = 4.5f;

    public void Initialize(RoomPartMark defaultMark)
    {
        if (SceneSearchService.TryFindNearest(transform.position, maxDistance, out RoomPartMark result))
        {
            partMark = result;
        }
        else
        {
            partMark = defaultMark;
        }
    }


}
