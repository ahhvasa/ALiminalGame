using System.Collections.Generic;
using System.Linq;

public class CreatureVisionSenseProvider : CreatureSenseProvider<VisibleObject, VisionSense>, ICreatureSenseProvider
{
    public RoomVision roomVision;

    public override void StartInternal()
    {

    }

    public override VisionSense GetSense(VisibleObject obj)
    {
        return new VisionSense(obj);
    }

    public override void UpdateValues()
    {
        List<Room> visibleRooms = RoomManadger.GetVisibleRooms(transform.position, roomVision);
        newObjects.Clear();
        foreach (Room room in visibleRooms)
        {
            newObjects.UnionWith(room.GetAllVisibleObjects().Where(item => item.AIIgnore == false));
        }
    }


}




public interface ICreatureSenseProvider
{
    void AddSenses(ref List<CreatureSense> creatureSenses);
}