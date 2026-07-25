using UnityEngine;

public class RandomRotationLerp : MonoBehaviour
{
    [Header("Rotation")]
    public Vector3 minRotation = new Vector3(-30f, -30f, -30f);
    public Vector3 maxRotation = new Vector3(30f, 30f, 30f);

    [Header("Speed")]
    public float rotationSpeed = 2f;
    public float arriveThreshold = 0.5f;

    private Quaternion targetRotation;

    private void Start()
    {
        PickNewRotation();
    }

    private void Update()
    {
        // Плавно вращаемся к цели
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        // Если почти достигли — выбираем новую
        float angle = Quaternion.Angle(transform.rotation, targetRotation);

        if (angle <= arriveThreshold)
        {
            PickNewRotation();
        }
    }

    private void PickNewRotation()
    {
        Vector3 randomEuler = new Vector3(
            Random.Range(minRotation.x, maxRotation.x),
            Random.Range(minRotation.y, maxRotation.y),
            Random.Range(minRotation.z, maxRotation.z)
        );

        targetRotation = Quaternion.Euler(randomEuler);
    }
}