using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureSoundSenseProvider : CreatureSenseProvider<Sound, SoundSense>, ICreatureSenseProvider
{
    public float hearingDistanceMultiplier = 1;

    List<VisibleObject> allVisibleObjects;

    public override void StartInternal()
    {
        allVisibleObjects = FindObjectsOfType<VisibleObject>().ToList();
    }

    public override SoundSense GetSense(Sound obj)
    {
        if (obj == null) return null;
        return new SoundSense(obj);
    }

    public override void UpdateValues()
    {
        newObjects.Clear();
        foreach (Sound sound in SoundManager.Instance.currentAIPerceivableActiveSounds)
        {
            if (Vector3.Distance(transform.position, sound.transform.position) < sound.aIPerceivedSoundData.soundDistance * hearingDistanceMultiplier)
            {
                newObjects.Add(sound);
            }
        }
    }
}




public class SmellSense : CreatureSense
{
    public ObjectSmell objectSmell;

    public SmellSense(ObjectSmell objectSmell) : base(objectSmell.perceivableObject, 0, objectSmell.perceivableObject.transform.position)
    {
        this.objectSmell = objectSmell;
    }

    public override bool EqualInternal(CreatureSense sense)
    {
        try
        {
            SmellSense soundSense = sense as SmellSense;
            return objectSmell == soundSense.objectSmell;
        }
        catch
        {
            return false;
        }
    }

    public override string ToString()
    {
        return $"[{base.ToString()}] {perceivableObject.gameObject.name} | lastUpdated = {lastTimeUpdated.ToString("F1")} | position = {position}";
    }

    public override void UpdateInternal(CreatureSense sense)
    {

    }
}

public interface IPercivableObject
{
    public PerceivableObject PerceivableObject { get; }
}

public abstract class CreatureSenseProvider<TObject, TSense> : MonoBehaviour
    where TObject : UnityEngine.Object, IPercivableObject
    where TSense : CreatureSense
{
    public event Action<TObject> OnDetectObject;
    public event Action<TObject> OnLoseObject;

    private HashSet<TObject> currentObjects = new();
    protected HashSet<TObject> newObjects = new();

    public void Start()
    {
        OnDetectObject +=
            (TObject visibleObject) =>
            {
                Debug.Log($"{base.ToString()} Detect Object -> {visibleObject}");
            };

        OnLoseObject +=
            (TObject visibleObject) =>
            {
                Debug.Log($"{base.ToString()} Lose Object -> {visibleObject}");
            };
        StartInternal();
    }

    public abstract void StartInternal();

    public void AddSenses(ref List<CreatureSense> creatureSenses)
    {
        foreach (TObject obj in currentObjects)
        {
            if (obj == null) { continue; }
            if (obj.PerceivableObject == null) { continue; }
            TSense sense = GetSense(obj);
            if (sense == null) { continue; }
            creatureSenses.Add(sense);
        }
    }

    public abstract TSense GetSense(TObject obj);

    public void FixedUpdate()
    {
        UpdateValues();
        Process();
    }

    public abstract void UpdateValues();

    public void Process()
    {
        List<TObject> removed = new List<TObject>();

        foreach (var item in currentObjects)
        {
            if (!newObjects.Contains(item))
            {
                removed.Add(item);
            }
        }

        if (removed != null)
        {
            foreach (var item in removed)
            {
                currentObjects.Remove(item);
                OnLoseObject?.Invoke(item);
            }
        }

        foreach (var item in newObjects)
        {
            if (currentObjects.Add(item))
            {
                OnDetectObject?.Invoke(item);
            }
        }
    }
}