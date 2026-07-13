using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureVisionSense : RoomVision, ICreatureSense
{
    public List<VisibleObject> visibleObjects = new();

    public event Action<VisibleObject> OnDetectObject;
    public event Action<VisibleObject> OnLoseObject;

    private readonly HashSet<VisibleObject> _currentObjects = new();
    private readonly HashSet<VisibleObject> _newObjects = new();

    public void Start()
    {
        OnDetectObject +=
            (VisibleObject visibleObject) =>
            {
                Debug.Log($"Vision Detect Object -> {visibleObject}");
            };


        OnLoseObject +=
            (VisibleObject visibleObject) =>
            {
                Debug.Log($"Vision Lose Object -> {visibleObject}");
            };
    }


    public void AddSenses(ref List<CreatureSense> creatureSenses)
    {
        foreach (var visibleObject in visibleObjects)
        {
            creatureSenses.Add(new VisionSense(visibleObject));
        }
    }

    void FixedUpdate()
    {
        List<Room> visibleRooms = GetVisibleRooms();
        _newObjects.Clear();
        foreach (Room room in visibleRooms)
        {
            _newObjects.UnionWith(room.GetAllVisibleObjects());
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







public interface ICreatureSense
{
    void AddSenses(ref List<CreatureSense> creatureSenses);
}