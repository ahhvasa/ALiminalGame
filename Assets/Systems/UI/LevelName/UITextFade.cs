using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITextFade : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float initialTime = 1f;
    public float fadeTime = 1f;
    public float showTime = 2f;

    public SoundData onTextShowSound;
    public SoundPlayer soundPlayer;

    private void Start()
    {
        StartCoroutine(ShowHideRoutine());
    }

    private IEnumerator ShowHideRoutine()
    {
        yield return new WaitForSeconds(initialTime);
        SoundManager.PlaySound(onTextShowSound, soundPlayer);

        Color transparent = text.color;
        transparent.a = 0f;

        Color visible = text.color;
        visible.a = 1f;

        text.color = transparent;

        yield return Fade(transparent, visible, fadeTime);
        yield return new WaitForSeconds(showTime);
        yield return Fade(visible, transparent, fadeTime);
    }

    private IEnumerator Fade(Color startColor, Color endColor, float time)
    {
        float t = 0f;

        while (t < time)
        {
            t += Time.deltaTime;
            text.color = Color.Lerp(startColor, endColor, t / time);
            yield return null;
        }

        text.color = endColor;
    }
}