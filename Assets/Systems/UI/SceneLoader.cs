using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private async void Start()
    {
        Instance = this;

        await ScreenFader.Instance.FadeFromBlackAsync();
    }

    public async Task LoadSceneAsync(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadOperation.allowSceneActivation = false;

        while (loadOperation.progress < 0.9f)
        {
            await Task.Yield();
        }
        await ScreenFader.Instance.FadeToBlackAsync();

        loadOperation.allowSceneActivation = true;

        while (!loadOperation.isDone)
        {
            await Task.Yield();
        }
    }
}