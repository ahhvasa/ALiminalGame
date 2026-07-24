using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance;

    public float fadeDuration = 1f;

    public Image image;

    public bool isFading;

    private void Awake()
    {
        Instance = this;

        SetTransparent();
    }

    public async Task FadeToBlackAsync()
    {
        await FadeAsync(0f, 1f);
    }

    public async Task FadeFromBlackAsync()
    {
        await FadeAsync(1f, 0f);
    }

    private async Task FadeAsync(float from, float to)
    {
        isFading = true;
        float timePassed = 0f;

        while (timePassed < fadeDuration)
        {
            timePassed += Time.deltaTime;

            float alpha = Mathf.Lerp(from, to, Mathf.Clamp01(timePassed / fadeDuration));

            Color color = image.color;
            color.a = alpha;
            image.color = color;

            await Task.Yield();
        }

        Color finalColor = image.color;
        finalColor.a = to;
        image.color = finalColor;
        isFading = false;
    }

    public void SetBlack()
    {
        Color color = image.color;
        color.a = 1f;
        image.color = color;
    }

    public void SetTransparent()
    {
        Color color = image.color;
        color.a = 0f;
        image.color = color;
    }
}
