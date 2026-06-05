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
}