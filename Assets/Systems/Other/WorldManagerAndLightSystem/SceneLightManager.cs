using System.Collections;
using UnityEngine;

public class SceneLightManager : MonoBehaviour
{
    public static SceneLightManager Instance;

    [SerializeField] private Light directionalLight;
    [SerializeField] private float transitionTime = 2f;

    private Coroutine lightCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    public void SetDay()
    {
        Debug.Log("Day");
        StartLightTransition(1f);
    }

    public void SetNight()
    {
        Debug.Log("Night");
        StartLightTransition(0.1f);
    }

    private void StartLightTransition(float targetIntensity)
    {
        if (lightCoroutine != null)
            StopCoroutine(lightCoroutine);

        lightCoroutine = StartCoroutine(ChangeLightIntensity(targetIntensity));
    }

    private IEnumerator ChangeLightIntensity(float targetIntensity)
    {
        float startIntensity = directionalLight.intensity;
        float time = 0f;

        while (time < transitionTime)
        {
            time += Time.deltaTime;
            directionalLight.intensity = Mathf.Lerp(
                startIntensity,
                targetIntensity,
                time / transitionTime);

            yield return null;
        }

        directionalLight.intensity = targetIntensity;
        lightCoroutine = null;
    }
}