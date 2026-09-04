using System.Threading.Tasks;


public interface ISaveSystem
{
    Task SaveAsync<T>(string key, T data);
    Task<T> LoadAsync<T>(string key);

    bool Exists(string key);
    void Delete(string key);
}
