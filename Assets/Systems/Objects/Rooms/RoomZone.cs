using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;

public class RoomZone : MonoBehaviour
{
    public List<VisibleObject> visibleObjects = new List<VisibleObject>();
    private List<VisibleObject> currentVisibleObjects = new List<VisibleObject>();

    public void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<VisibleObject>(out var visibleObject))
        {
            if (currentVisibleObjects.Contains(visibleObject) == false)
            {
                currentVisibleObjects.Add(visibleObject);
            }
        }
    }
    public void FixedUpdate()
    {
        visibleObjects.Clear();
        visibleObjects.AddRange(currentVisibleObjects);
        currentVisibleObjects.Clear();
    }

    public void Show(bool show)
    {
        foreach (var visibleObject in visibleObjects)
        {
            visibleObject.Show(show);
        }
    }
}