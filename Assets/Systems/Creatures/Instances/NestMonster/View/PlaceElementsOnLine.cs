using System.Collections.Generic;
using UnityEngine;

public class PlaceElementsOnLine : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject prefab;

    public ObjectPull<GameObject> objectPull;
    public int initialCount = 20;

    public List<GameObject> elements = new();

    public GoToPosition lastObject;

    [Range(0f, 1f)]
    public float elementOffsetT;
    public float elementOffsetT_changeSpeed = 1f;

    public Transform elementRoot;

    private void Start()
    {
        objectPull = new ObjectPull<GameObject>(prefab, initialCount);
        RefreshObjects();
    }

    private void Update()
    {
        RefreshObjects();
        UpdatePositions();

        elementOffsetT += Time.deltaTime * elementOffsetT_changeSpeed;
        if (elementOffsetT > 1) { elementOffsetT = elementOffsetT - 1; }
    }

    private void RefreshObjects()
    {
        int pointCount = lineRenderer.positionCount;

        while (elements.Count < pointCount)
        {
            GameObject obj = objectPull.GetObject();
            obj.transform.SetParent(elementRoot, true);
            obj.transform.position = transform.position;
            elements.Add(obj);
        }
        while (elements.Count > pointCount)
        {
            objectPull.ReturnObject(elements[^1]);
            elements.RemoveAt(elements.Count - 1);
        }
    }

    private void UpdatePositions()
    {
        int pointCount = lineRenderer.positionCount;

        for (int i = 0; i < pointCount; i++)
        {
            Vector3 current = lineRenderer.useWorldSpace
                ? lineRenderer.GetPosition(i)
                : lineRenderer.transform.TransformPoint(lineRenderer.GetPosition(i));

            if (i == pointCount - 1)
            {
                elements[i].transform.position = current;
                continue;
            }

            Vector3 next = lineRenderer.useWorldSpace
                ? lineRenderer.GetPosition(i + 1)
                : lineRenderer.transform.TransformPoint(lineRenderer.GetPosition(i + 1));

            elements[i].transform.position = Vector3.Lerp(current, next, elementOffsetT);


            if (i == 0)
            {
                lastObject.SetTarget(current);
            }
        }

    }
}
