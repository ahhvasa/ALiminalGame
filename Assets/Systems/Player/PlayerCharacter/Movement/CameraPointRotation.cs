using UnityEngine;

public class CameraPointRotation : MonoBehaviour
{
    public float minSpeed = 30f;
    public float maxSpeed = 360f;
    public float acceleration = 2f;

    private Quaternion currentRotation;
    public Quaternion targetRotation;

    public void Start()
    {
        currentRotation = transform.rotation;
        targetRotation = currentRotation;
    }

    public void SetTargetRotation(Quaternion rotation)
    {
        targetRotation = rotation;
    }

    public void Update()
    {
        float angle = Quaternion.Angle(currentRotation, targetRotation);

        float t = Mathf.Clamp01(angle / 180f);
        float speed = Mathf.Lerp(minSpeed, maxSpeed, t);

        currentRotation = Quaternion.RotateTowards(
            currentRotation,
            targetRotation,
            speed * Time.deltaTime
        );

        transform.rotation = currentRotation;  
    }

    public Quaternion GetRotation()
    {
        return currentRotation;
    }
}