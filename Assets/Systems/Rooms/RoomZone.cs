using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomZone : MonoBehaviour
{
    public List<VisibleObject> visibleObjects;
    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<VisibleObject>(out var visibleObject))
        {
            visibleObjects.Add(visibleObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {

        if (other.TryGetComponent<VisibleObject>(out var visibleObject))
        {
            if (visibleObjects.Contains(visibleObject))
            {
                visibleObjects.Remove(visibleObject);
            }
        }
    }
    public void Show(bool show)
    {
        foreach (var visibleObject in visibleObjects)
        {
            visibleObject.Show(show);
        }
    }
}