using UnityEngine;

public class ScaleController : MonoBehaviour
{
    public Vector3 initialScale;
    public Vector3 targetScale;
    public float speed = 5f;

    private void Awake()
    {
        initialScale = transform.localScale;
        targetScale = initialScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(
            transform.localScale,
            targetScale,
            speed * Time.deltaTime
        );
    }
}