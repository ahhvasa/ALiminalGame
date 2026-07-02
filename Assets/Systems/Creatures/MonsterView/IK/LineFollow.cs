using UnityEngine;

public class LineFollow : MonoBehaviour
{
    public float speed;
    public float t;
    public LineRenderer lineRenderer;
    public Transform target;
    public void FixedUpdate()
    {
        t += speed * Time.deltaTime;
        t = Mathf.Abs(t) % 1;
        target.transform.position = LineExtention.GetPoint(lineRenderer, t);
    }
}
