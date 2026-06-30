using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;


public class VisibleObject : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.3f;

    [SerializeField] private List<MeshRenderer> meshRenderers = new();
    [SerializeField] private List<SpriteRenderer> spriteRenderers = new();
    [SerializeField] private List<Light> lightSources = new();

    [SerializeField] private List<GameObject> connectedObjects;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        meshRenderers.AddRange(GetComponentsInChildren<MeshRenderer>(true));
        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>(true));
        lightSources.AddRange(GetComponentsInChildren<Light>(true));
        
    }
    public void ConnectObject(GameObject gameObject)
    {
        connectedObjects.Add(gameObject);

        meshRenderers.AddRange(gameObject.GetComponentsInChildren<MeshRenderer>(true));
        spriteRenderers.AddRange(gameObject.GetComponentsInChildren<SpriteRenderer>(true));
        lightSources.AddRange(gameObject.GetComponentsInChildren<Light>(true));
    }

    public void Show(bool show)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (gameObject.activeSelf == false)
        { return; }

        fadeCoroutine = StartCoroutine(Fade(show));
    }

    public void OnDisable()
    {
        StopCoroutine(fadeCoroutine);
    }

    private IEnumerator Fade(bool show)
    {
        float startAlpha = GetCurrentAlpha();
        float targetAlpha = show ? 1f : 0f;

        if (show)
            SetRenderersEnabled(true);

        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            float t = Mathf.Clamp01(time / fadeDuration);
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(targetAlpha);

        if (!show)
            SetRenderersEnabled(false);

        fadeCoroutine = null;
    }

    private float GetCurrentAlpha()
    {
        if (spriteRenderers.Count > 0)
            return spriteRenderers[0].color.a;

        if (meshRenderers.Count > 0)
            return meshRenderers[0].material.color.a;

        if (lightSources.Count > 0)
            return lightSources[0].enabled ? 1 : 0;

        return 1f;
    }

    private void SetAlpha(float alpha)
    {
        foreach (var renderer in spriteRenderers)
        {
            Color color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }

        foreach (var renderer in meshRenderers)
        {
            Material material = renderer.material;

            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }

        foreach (var lightSource in lightSources)
        {
            lightSource.enabled = alpha > 0.5f ? true : false;
        }

    }

    private void SetRenderersEnabled(bool enabled)
    {
        foreach (var renderer in spriteRenderers)
            renderer.enabled = enabled;

        foreach (var renderer in meshRenderers)
            renderer.enabled = enabled;

        foreach (var lightSource in lightSources)
            lightSource.enabled = enabled;
    }
}