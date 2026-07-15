using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour, IPlayerInteractableObject
{
    public string sceneName;

    public void Interact(Player player)
    {
        SceneManager.LoadScene(sceneName);
    }
}
