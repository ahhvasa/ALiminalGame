using UnityEngine;

public class DirectionSelector : MonoBehaviour
{
    public Transform targetTransform;

    public Transform[] points = new Transform[4];
    public GameObject[] objects = new GameObject[4];

    void Update()
    {
        int bestIndex = -1;
        float bestDot = -Mathf.Infinity;

        for (int i = 0; i < points.Length; i++)
        {
            if (points[i] == null) continue;

            Vector3 dir = (points[i].position - transform.position).normalized;
            float dot = Vector3.Dot(targetTransform.forward, dir);

            Debug.DrawRay(transform.position, (points[i].position - transform.position).normalized, Color.green);

            Debug.DrawRay(transform.position, targetTransform.forward, Color.red);

            if (dot > bestDot)
            {
                bestDot = dot;
                bestIndex = i;
            }
        }
        Debug.Log("563 " + bestIndex);
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
                objects[i].SetActive(i == bestIndex);
        }
    }
}