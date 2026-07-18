using UnityEngine;
using System.Collections.Generic;

public class MoveLineScalerOnStop : MonoBehaviour
{
    public IKLegController iKLegController;

    public GameObject[] moveLines;
    private List<ScaleController> scaleControllers = new List<ScaleController>();

    private void Awake()
    {
        foreach (var moveLine in moveLines)
        {
            scaleControllers.Add(moveLine.AddComponent<ScaleController>());
        }
    }

    private void FixedUpdate()
    {
        bool isMoving = iKLegController.speed > 0.01f;

        foreach (var scaleController in scaleControllers)
        {
            scaleController.targetScale = isMoving ? scaleController.initialScale : Vector3.zero;
        }
    }
}