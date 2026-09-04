using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureVisionSenseProvider : CreatureSenseProvider<VisibleObject, VisionSense>, ICreatureSenseProvider
{
    public RoomVision roomVision;
    public LayerMask layerMask;
    public float maxDistance = 25;

    public override void StartInternal()
    {

    }

    public override VisionSense GetSense(VisibleObject obj)
    {
        return new VisionSense(obj);
    }

    public override void UpdateValues()
    {
        List<Room> visibleRooms = RoomManager.GetVisibleRooms(transform.position, roomVision);
        newObjects.Clear();
        foreach (Room room in visibleRooms)
        {
            newObjects.UnionWith(room.GetAllVisibleObjects().Where(item => item.AIIgnore == false));
        }

        newObjects.RemoveWhere(item => CanSeeObject(item) == false);
    }

    public bool CanSeeObject(VisibleObject visibleObject)
    {
        Vector3 direction = visibleObject.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance > maxDistance) { return false; }

        if (Physics.Raycast(transform.position, direction.normalized, distance, layerMask))
        {
            Debug.DrawRay(transform.position, direction.normalized * distance, Color.red);
            return false;
        }
        else
        {
            Debug.DrawRay(transform.position, direction.normalized * distance, Color.green);
            return true;
        }
    }


}




public interface ICreatureSenseProvider
{
    void AddSenses(ref List<CreatureSense> creatureSenses);
}