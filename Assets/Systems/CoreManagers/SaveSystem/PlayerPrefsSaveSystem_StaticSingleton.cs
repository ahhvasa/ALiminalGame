using System.Threading.Tasks;
using UnityEngine;

public class PlayerPrefsSaveSystem_StaticSingleton : MonoBehaviour
{
    public static PlayerPrefsSaveSystem_StaticSingleton Instance;
    public void Awake()
    {
        Instance = this;
    }

    public Task SaveAsync<T>(string key, T data)
    {
        string json = JsonUtility.ToJson(data, true);

        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();

        return Task.CompletedTask;
    }

    public Task<T> LoadAsync<T>(string key)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            return Task.FromResult(default(T));
        }

        string json = PlayerPrefs.GetString(key);
        T data = JsonUtility.FromJson<T>(json);

        return Task.FromResult(data);
    }

    public bool Exists(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public void Delete(string key)
    {
        if (!PlayerPrefs.HasKey(key))
            return;

        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }
}
