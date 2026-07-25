using UnityEngine;

public class GoToPosition : MonoBehaviour
{
    public float maximumDistance = 3;

    public Vector3 position;
    public float speed = 5f;

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, position) > maximumDistance)
        {
            transform.position = position + ((position - transform.position).normalized * maximumDistance);
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            position,
            speed * Time.deltaTime);

    }

    public void SetTarget(Vector3 position)
    {
        this.position = position;
    }
}