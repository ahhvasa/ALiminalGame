using System.Collections.Generic;
using UnityEngine;

public class CreatureMemory : MonoBehaviour
{
    public CreatureVision creatureVision;

    public List<CreatureSense> senses = new();
    public List<CreatureSense> currentSenses = new();

    [SerializeField][TextArea(10, 50)] private string memoryLog;

    public void FixedUpdate()
    {
        var visibleObjects = creatureVision.visibleObjects;

        currentSenses.Clear();

        foreach (var visibleObject in visibleObjects)
        {
            currentSenses.Add(new VisionSense(visibleObject));
        }

        UpdateSenses();
        memoryLog = senses.ListToText<CreatureSense>();
        SenseTimer();
    }

    public void UpdateSenses()
    {
        List< CreatureSense > remainingSenses = new List< CreatureSense >();
        remainingSenses.AddRange(currentSenses);

        foreach (var sense in senses)
        {
            foreach (var currentSense in currentSenses)
            {
                if (sense.Equal(currentSense))
                {
                    sense.Update(currentSense);
                    remainingSenses.Remove(currentSense);
                    break;
                }
            }
        }

        foreach (var remainingSense in remainingSenses)
        {
            senses.Add(remainingSense);
        }
    }

    public float memoryTime = 5f;
    public void SenseTimer()
    {
        for (int i = 0; i != senses.Count; i ++)
        {
            var sense = senses[i];
            sense.lastTimeUpdated += Time.deltaTime;
            if (sense.lastTimeUpdated >= memoryTime)
            {
                senses.Remove(sense);
                i--;
            }
        }
    }
}
