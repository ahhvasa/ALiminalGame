using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class TextFade : MonoBehaviour
{
    public TMP_Text text;
    public float keepTime = 1f;
    public float fadeTime = 1f;

    public event Action OnFadeFinish;

    private void OnEnable()
    {
        StartCoroutine(Fade());
    }

    private System.Collections.IEnumerator Fade()
    {
        Color color = text.color;
        float startAlpha = 1f;
        float time = 0f;

        yield return new WaitForSeconds(keepTime);

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            float t = fadeTime <= 0f ? 1f : time / fadeTime;
            color.a = Mathf.Lerp(startAlpha, 0f, t);
            text.color = color;

            yield return null;
        }

        color.a = 0f;
        text.color = color;

        OnFadeFinish?.Invoke();
    }
}
