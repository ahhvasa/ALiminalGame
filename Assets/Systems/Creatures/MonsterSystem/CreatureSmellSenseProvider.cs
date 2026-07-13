using UnityEngine;

public class CreatureSmellSenseProvider : CreatureSenseProvider<ObjectSmell, SmellSense>, ICreatureSenseProvider
{
    public float smellDistance = 10;

    public override void StartInternal()
    {
    }

    public override SmellSense GetSense(ObjectSmell obj)
    {
        return new SmellSense(obj);
    }

    public override void UpdateValues()
    {
        newObjects.Clear();
        foreach (var objectSmell in ObjectSmellManadger.Instance.objectSmell)
        {
            if (objectSmell.AIIgnore) { continue; }
            if (Vector3.Distance(transform.position, objectSmell.transform.position) < smellDistance)
            {
                newObjects.Add(objectSmell);
            }
        }
    }
}
