using UnityEngine;

public class GoToPosition : MonoBehaviour
{
    public Vector3 position;
    public float speed = 5f;

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