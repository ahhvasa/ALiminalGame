using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CreatureVision : RoomVision
{
    public List<VisibleObject> visibleObjects = new();

    public event Action<VisibleObject> OnSawObject;
    public event Action<VisibleObject> OnLoseObject;

    private readonly HashSet<VisibleObject> _currentObjects = new();
    private readonly HashSet<VisibleObject> _newObjects = new();

    public void Start()
    {
        OnSawObject +=
            (VisibleObject visibleObject) =>
            {
                Debug.Log($"Saw Object -> {visibleObject}");
            };


        OnLoseObject +=
            (VisibleObject visibleObject) =>
            {
                Debug.Log($"Lose Object -> {visibleObject}");
            };
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
            if (_currentObjects.Add(obj))
            {
                OnSawObject?.Invoke(obj);
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