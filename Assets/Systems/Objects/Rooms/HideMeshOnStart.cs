using UnityEngine;

public class HideMeshOnStart : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public void Awake()
    {
        DestroyImmediate(meshRenderer);
    }
}
