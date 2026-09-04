using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject.SpaceFighter;

public class WinScreen : MonoBehaviour
{
    public static WinScreen Instance;
    public Player player;
    public GameMenu gameMenu;

    public void Awake()
    {
        Instance = this;
        this.enabled = false;
    }

    public async void Activate()
    {
        gameMenu.enabled = false;
        enabled = true;

        Fade(backGround, 0, 1, 0.5f);
        await Fade(winScreen, 0, 1, 0.5f);

        await Fade(winScreen, 1, 1, 1f);


        await Fade(winScreen, 1, 0, 0.5f);
        await Fade(creditsScreen, 0, 1, 0.5f);

        await Fade(creditsScreen, 1, 1, 1f);

        showedCredits = true;

    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (showedCredits)
            {
                Application.Quit();
            }
        }
    }

    public bool showedCredits = false;

    public Image backGround;
    public TextMeshProUGUI winScreen;
    public TextMeshProUGUI creditsScreen;





    private async Task Fade(TextMeshProUGUI text, float initialAlpha, float targetAlpha, float fadeTime)
    {
        Color color = text.color;
        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            float t = fadeTime <= 0f ? 1f : time / fadeTime;
            color.a = Mathf.Lerp(initialAlpha, targetAlpha, t);
            text.color = color;

            await Task.Yield();
        }

        color.a = targetAlpha;
        text.color = color;
    }

    private async Task Fade(Image image, float initialAlpha, float targetAlpha, float fadeTime)
    {
        Color color = image.color;
        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;

            float t = fadeTime <= 0f ? 1f : time / fadeTime;
            color.a = Mathf.Lerp(initialAlpha, targetAlpha, t);
            image.color = color;

            await Task.Yield();
        }

        color.a = targetAlpha;
        image.color = color;
    }


}
