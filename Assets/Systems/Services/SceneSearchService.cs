using System.Collections.Generic;
using UnityEngine;

public static class SceneSearchService
{
    public static bool TryFindNearest<T>(
        Vector3 position,
        float maxDistance,
        out T result)
        where T : Component
    {
        result = null;

        T[] objects = Object.FindObjectsOfType<T>();

        float closestDistance = maxDistance;

        foreach (T obj in objects)
        {
            float distance = Vector3.Distance(position, obj.transform.position);

            if (distance <= closestDistance)
            {
                closestDistance = distance;
                result = obj;
            }
        }

        return result != null;
    }


    public static List<T> FindAllObjectsInSquareZone<T>(Vector3 position, float squareSize)
        where T : Component
    {
        List<T> objectsInRange = new List<T>();
        T[] allObjects = MonoBehaviour.FindObjectsByType<T>(FindObjectsSortMode.None);

        float halfSize = squareSize * 0.5f;

        foreach (var currentObject in allObjects)
        {
            Vector3 pos = currentObject.transform.position;
            Vector3 center = position;

            if (Mathf.Abs(pos.x - center.x) <= halfSize &&
                Mathf.Abs(pos.z - center.z) <= halfSize)
            {
                objectsInRange.Add(currentObject);
            }
        }
        return objectsInRange;
    }






}