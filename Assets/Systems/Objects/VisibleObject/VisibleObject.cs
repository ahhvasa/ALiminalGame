using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;


public class VisibleObject : MonoBehaviour, IPercivableObject
{
    public PerceivableObject PerceivableObject
    {
        get
        {
            return perceivableObject;
        }
    }

    public PerceivableObject perceivableObject;
    /// <summary>
    /// A flag that indicates whether the object will be taken into account by the AI.
    /// </summary>
    [Tooltip("A flag that indicates whether the object will be taken into account by the AI.")] public bool AIIgnore;

    [SerializeField] private float fadeDuration = 0.3f;

    [SerializeField] private List<MeshRenderer> meshRenderers = new();
    [SerializeField] private List<SpriteRenderer> spriteRenderers = new();
    [SerializeField] private List<Light> lightSources = new();
    [SerializeField] private List<VO_HideGameObjectOnZeroAlpha> hideGameObjectsOnZeroAlpha = new();
    

    [SerializeField] private List<GameObject> connectedObjects;

    public float CurrentProgress { get { return _currentProgress; } }
    private float _currentProgress = 0;

    private Coroutine fadeCoroutine;


    public void PostProcessLists()
    {
        for (int i = 0; i != meshRenderers.Count; i++)
        {
            if (meshRenderers[i].gameObject.GetComponent<VO_DontIncludeComponentsMark>() != null)
            {
                meshRenderers.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i != spriteRenderers.Count; i++)
        {
            if (spriteRenderers[i].gameObject.GetComponent<VO_DontIncludeComponentsMark>() != null)
            {
                spriteRenderers.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i != lightSources.Count; i++)
        {
            if (lightSources[i].gameObject.GetComponent<VO_DontIncludeComponentsMark>() != null)
            {
                lightSources.RemoveAt(i);
                i--;
            }
        }
        for (int i = 0; i != hideGameObjectsOnZeroAlpha.Count; i++)
        {
            if (hideGameObjectsOnZeroAlpha[i].gameObject.GetComponent<VO_DontIncludeComponentsMark>() != null)
            {
                hideGameObjectsOnZeroAlpha.RemoveAt(i);
                i--;
            }
        }
    }



    private void Awake()
    {
        meshRenderers.AddRange(GetComponentsInChildren<MeshRenderer>(true));
        spriteRenderers.AddRange(GetComponentsInChildren<SpriteRenderer>(true));
        lightSources.AddRange(GetComponentsInChildren<Light>(true));
        hideGameObjectsOnZeroAlpha.AddRange(GetComponentsInChildren<VO_HideGameObjectOnZeroAlpha>(true));
        PostProcessLists();

        if (perceivableObject == null)
        {
            if (gameObject.TryGetComponent<PerceivableObject>(out perceivableObject) == false)
            {
                perceivableObject = gameObject.AddComponent<PerceivableObject>();
            }
        }
        perceivableObject.visibleObject = this;

        SetAlpha(0);
    }
    public void ConnectObject(GameObject gameObject)
    {
        connectedObjects.Add(gameObject);

        meshRenderers.AddRange(gameObject.GetComponentsInChildren<MeshRenderer>(true));
        spriteRenderers.AddRange(gameObject.GetComponentsInChildren<SpriteRenderer>(true));
        lightSources.AddRange(gameObject.GetComponentsInChildren<Light>(true));
        hideGameObjectsOnZeroAlpha.AddRange(gameObject.GetComponentsInChildren<VO_HideGameObjectOnZeroAlpha>(true));

        PostProcessLists();
    }
    public void DisconnectObject(GameObject gameObject)
    {
        connectedObjects.Remove(gameObject);

        foreach (var item in gameObject.GetComponentsInChildren<MeshRenderer>(true))
            meshRenderers.Remove(item);

        foreach (var item in gameObject.GetComponentsInChildren<SpriteRenderer>(true))
            spriteRenderers.Remove(item);

        foreach (var item in gameObject.GetComponentsInChildren<Light>(true))
            lightSources.Remove(item);

        foreach (var item in gameObject.GetComponentsInChildren<VO_HideGameObjectOnZeroAlpha>(true))
            hideGameObjectsOnZeroAlpha.Remove(item);

        PostProcessLists();
    }

    public void Show(bool show)
    {
        if (this == null) { return; }

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (gameObject.activeSelf == false || gameObject.activeInHierarchy == false)
        { return; }

        fadeCoroutine = StartCoroutine(Fade(show));
    }

    public void OnDisable()
    {
        if (fadeCoroutine == null) { return; }
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
            _currentProgress = alpha;
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

        if (hideGameObjectsOnZeroAlpha.Count > 0)
            return hideGameObjectsOnZeroAlpha[0].gameObject.activeSelf ? 1 : 0;

        return 1f;
    }

    private float _currentAlpha = 0;
    public float CurrentAlpha
    {
        get
        {
            return _currentAlpha;
        }
    }
    private void SetAlpha(float alpha)
    {
        _currentAlpha = alpha;
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

        foreach (var gameObject in hideGameObjectsOnZeroAlpha)
        {
            gameObject.gameObject.SetActive(alpha > 0.5f ? true : false);
            if (gameObject.scaleSize)
            {
                gameObject.gameObject.transform.localScale = new Vector3(alpha, alpha, alpha);
            }
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

        foreach (var gameObject in hideGameObjectsOnZeroAlpha)
            gameObject.enabled = enabled;
    }
}
