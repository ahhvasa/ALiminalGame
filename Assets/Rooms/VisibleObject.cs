using System.Collections;
using UnityEngine;

public class VisibleObject : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 0.3f;

    private MeshRenderer[] meshRenderers;
    private SpriteRenderer[] spriteRenderers;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);
    }

    public void Show(bool show)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(Fade(show));
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
        if (spriteRenderers.Length > 0)
            return spriteRenderers[0].color.a;

        if (meshRenderers.Length > 0)
            return meshRenderers[0].material.color.a;

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
    }

    private void SetRenderersEnabled(bool enabled)
    {
        foreach (var renderer in spriteRenderers)
            renderer.enabled = enabled;

        foreach (var renderer in meshRenderers)
            renderer.enabled = enabled;
    }
}