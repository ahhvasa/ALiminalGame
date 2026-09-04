using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class AutoTextureTiling : MonoBehaviour
{
    [SerializeField] private float tilesPerUnit = 1f;
    [SerializeField] private bool updateInPlayMode;

    private Renderer _renderer;
    private Material _material;

    private void OnEnable()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.material;
        UpdateTiling();
    }

    private void Update()
    {
        if (updateInPlayMode)
            UpdateTiling();
    }

    private void UpdateTiling()
    {
        Vector3 scale = transform.lossyScale;

        Vector2 tiling = _material.mainTextureScale;

        _material.mainTextureScale = new Vector2(scale.x, scale.y) * tilesPerUnit * tiling;
    }
}