using UnityEngine;

public class ConnectToVisibleObject : MonoBehaviour
{
    public VisibleObject visibleObject;
    public void Start()
    {
        visibleObject.ConnectObject(gameObject);
    }
}
