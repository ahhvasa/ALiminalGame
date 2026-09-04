using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelPortal : MonoBehaviour
{
    public string sceneName;

    public bool activated = false;

    public void Awake()
    {
        activated = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            LoadLevel();
        }
    }

    public async void LoadLevel()
    {
        if (activated) { return; }
        activated = true;

        try
        {

            if (markPlayerProgress)
            {
                await SaveProgress();
            }
            await SceneLoader.Instance.LoadSceneAsync(sceneName);
            activated = false;
        }
        catch
        {
            activated = false;
        }

    }

    public async Task SaveProgress()
    {
        PlayerProgress file = await PlayerPrefsSaveSystem_StaticSingleton.Instance.LoadAsync<PlayerProgress>(PlayerProgress.fileName);
        if (file == null) { file = new PlayerProgress(); }
        file.finishedLevels.Add(SceneManager.GetActiveScene().name);

        await PlayerPrefsSaveSystem_StaticSingleton.Instance.SaveAsync<PlayerProgress>(PlayerProgress.fileName, file);
    }

    public bool markPlayerProgress = false;
}
