using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class CreatureMemory : MonoBehaviour
{
    public ICreatureSenseProvider[] creatureSenseProviders;

    public bool TryGetProvider<T>(out T result)
        where T : UnityEngine.Object, ICreatureSenseProvider
    {
        foreach (var creatureSenseProvider in creatureSenseProviders)
        {
            if (creatureSenseProvider is T)
            {
                result =  creatureSenseProvider as T;
                return true; 
            }
        }
        result = null;
        return false;
    }

    public List<CreatureSense> senses = new();
    public List<CreatureSense> currentSenses = new();

    [SerializeField][TextArea(10, 50)] private string memoryLog;

    public void Awake()
    {
        creatureSenseProviders = GetComponentsInChildren<ICreatureSenseProvider>();
    }

    public void FixedUpdate()
    {
        currentSenses.Clear();
        foreach (var creatureSense in creatureSenseProviders)
        {
            creatureSense.AddSenses(ref currentSenses);
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
