using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelPortal : MonoBehaviour
{
    [Inject] public ISaveSystem saveSystem;
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LoadLevel();
        }
    }

    public async void LoadLevel()
    {
        if (markPlayerProgress)
        {
            await SaveProgress();
        }
        await SceneLoader.Instance.LoadSceneAsync(sceneName);
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
