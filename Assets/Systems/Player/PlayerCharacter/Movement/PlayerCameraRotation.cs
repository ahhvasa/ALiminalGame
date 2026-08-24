using UnityEngine;

public class PlayerCameraRotation : MonoBehaviour
{
    public CameraPointRotation cameraPointRotation;

    public Vector3[] rotations =
    {
        new Vector3(45f, 45f, 0f),
        new Vector3(45f, 90f, 0f),
        new Vector3(45f, 135f, 0f),
        new Vector3(45f, 180f, 0f),
        new Vector3(45f, 225f, 0f),
        new Vector3(45f, 270f, 0f),
        new Vector3(45f, 315f, 0f),
        new Vector3(45f, 0f, 0f)
    };

    private int currentIndex;

    public int CurrentIndex
    {
        get => currentIndex;

        set
        {
            if (rotations.Length == 0)
                return;

            currentIndex = value % rotations.Length;

            if (currentIndex < 0)
                currentIndex += rotations.Length;

            CurrentRotation = Quaternion.Euler(rotations[currentIndex]);
        }
    }

    public Quaternion CurrentRotation
    {
        set
        {
            cameraPointRotation.SetTargetRotation(value);
        }
    }

    public void Start()
    {
        CurrentIndex = 0;
    }

    public void Update()
    {
        if (InputProvider.RotateCameraLeft())
        {
            CurrentIndex--;
        }
        else if (InputProvider.RotateCameraRight())
        {
            CurrentIndex++;
        }
    }
}
