using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerObjectHold : MonoBehaviour
{
    public Player player;
    public Transform parent;
    public Vector3 offset;

    Transform currentTargrt;
    public void HoldObject(Transform target)
    {
        DropObject();
        currentTargrt = target;
        target.SetParent(parent.transform);
        target.localPosition = Vector3.zero + offset;
    }
    public void DropObject()
    {
        currentTargrt?.SetParent(null);
        currentTargrt = null;
    }
}




public class Test144444 : MonoBehaviour
{

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = LayerMask.GetMask("GroundQuad");

        if (Physics.Raycast(ray, 100, layerMask))
        {
            Debug.Log("Something");
        }
    }

}