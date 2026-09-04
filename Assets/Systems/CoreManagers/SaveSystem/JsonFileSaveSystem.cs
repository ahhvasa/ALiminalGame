using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class JsonFileSaveSystem : ISaveSystem
{
    private readonly string _saveFolder;

    public JsonFileSaveSystem(string saveFolder = null)
    {
        _saveFolder = saveFolder ?? Path.Combine(Application.persistentDataPath, "Saves");

        if (!Directory.Exists(_saveFolder))
            Directory.CreateDirectory(_saveFolder);
    }

    public async Task SaveAsync<T>(string key, T data)
    {
        string path = GetPath(key);

        string json = JsonUtility.ToJson(data, true);

        using StreamWriter writer = new StreamWriter(path, false);
        await writer.WriteAsync(json);
    }

    public async Task<T> LoadAsync<T>(string key)
    {
        string path = GetPath(key);

        if (!File.Exists(path))
        {
            Debug.LogWarning($"Save file not found: {path}");
            return default;
        }

        using StreamReader reader = new StreamReader(path);
        string json = await reader.ReadToEndAsync();

        return JsonUtility.FromJson<T>(json);
    }

    public bool Exists(string key)
    {
        return File.Exists(GetPath(key));
    }

    public void Delete(string key)
    {
        string path = GetPath(key);

        if (File.Exists(path))
            File.Delete(path);
    }

    private string GetPath(string key)
    {
        return Path.Combine(_saveFolder, $"{key}.json");
    }
}