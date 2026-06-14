using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PrefabFactory
{
    public async Task<GameObject> LoadPrefabAsync(string key)
    {
        AsyncOperationHandle<GameObject> handle =
            Addressables.LoadAssetAsync<GameObject>(key);

        GameObject prefab = await handle.Task;

        return GameObject.Instantiate(prefab);
    }

    public GameObject LoadPrefabFromResources(string key)
    {
        GameObject gameObjectPrototype = Resources.Load<GameObject>(key);
        return GameObject.Instantiate(gameObjectPrototype);
    }

    public void Destroy(GameObject gameObject)
    {
        GameObject.Destroy(gameObject);
    }
}