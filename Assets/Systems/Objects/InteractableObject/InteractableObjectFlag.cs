using UnityEngine;

public class InteractableObjectFlag : MonoBehaviour
{
    public bool active = true;
    public float objectActivationDistance = 2.5f;
    public VisibleObject visibleObject;
    public float playerActivationLabelHeight = 0;

    public void Awake()
    {
        visibleObject = GetComponentInParent<VisibleObject>();
    }
}
