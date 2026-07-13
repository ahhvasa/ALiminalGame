using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureSoundSense : RoomVision, ICreatureSense
{
    public List<VisibleObject> visibleObjects = new();

    public float hearingDistance = 5;

    public event Action<VisibleObject> OnDetectObject;
    public event Action<VisibleObject> OnLoseObject;

    private readonly HashSet<VisibleObject> _currentObjects = new();
    private readonly HashSet<VisibleObject> _newObjects = new();

    List<VisibleObject> allVisibleObjects;
    public void Start()
    {
        allVisibleObjects = FindObjectsOfType<VisibleObject>().ToList();

        OnDetectObject +=
            (VisibleObject visibleObject) =>
            {
                Debug.Log($"Heard Detect Object -> {visibleObject}");
            };


        OnLoseObject +=
            (VisibleObject visibleObject) =>
            {
                Debug.Log($"Heard Lose Object -> {visibleObject}");
            };
    }


    public void AddSenses(ref List<CreatureSense> creatureSenses)
    {
        foreach (var visibleObject in visibleObjects)
        {
            creatureSenses.Add(new SoundSense(visibleObject));
        }
    }

    void FixedUpdate()
    {
        _newObjects.Clear();
        foreach (var visibleObject in allVisibleObjects)
        {
            if (visibleObject.AIIgnore) { continue; }
            if (Vector3.Distance(transform.position, visibleObject.transform.position) < hearingDistance)
            {
                _newObjects.Add(visibleObject);
            }
        }

        foreach (VisibleObject obj in _newObjects)
        {
            if (obj.AIIgnore) { continue; }
            if (_currentObjects.Add(obj))
            {
                OnDetectObject?.Invoke(obj);
            }
        }

        foreach (VisibleObject obj in _currentObjects.ToArray())
        {
            if (!_newObjects.Contains(obj))
            {
                _currentObjects.Remove(obj);
                OnLoseObject?.Invoke(obj);
            }
        }

        visibleObjects.Clear();
        visibleObjects.AddRange(_currentObjects);





    }


}
