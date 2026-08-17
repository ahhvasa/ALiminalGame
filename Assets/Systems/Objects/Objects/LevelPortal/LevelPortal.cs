using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelPortal : MonoBehaviour
{
    [Inject] public ISaveSystem saveSystem;
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
        Debug.Log($"LEVEL PORTAL: LoadLevel activated. And currently loading {activated}  name {sceneName}");

        if (activated) { return; }
        activated = true;

        try
        {

            if (markPlayerProgress)
            {
                await SaveProgress();
            }
            await SceneLoader.Instance.LoadSceneAsync(sceneName);
            Debug.Log($"LEVEL PORTAL: Must be loaded");
            activated = false;
        }
        catch
        {
            Debug.Log("LEVEL PORTAL: ");
            activated = false;
        }

    }

    public async Task SaveProgress()
    {
        PlayerProgress file = await saveSystem.LoadAsync<PlayerProgress>(PlayerProgress.fileName);
        if (file == null) { file = new PlayerProgress(); }
        file.finishedLevels.Add(SceneManager.GetActiveScene().name);

        await saveSystem.SaveAsync<PlayerProgress>(PlayerProgress.fileName, file);
    }

    public bool markPlayerProgress = false;
}
