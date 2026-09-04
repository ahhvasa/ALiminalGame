using UnityEngine;
using Zenject;

public class SaveExample : MonoBehaviour
{
    [Inject] private ISaveSystem _saveSystem;

    private async void Start()
    {
        //var progress = new PlayerProgress
        //{
        //    Level = 5,
        //    Coins = 1200
        //};

        //await _saveSystem.SaveAsync("player_progress", progress);

        //PlayerProgress loaded =
        //    await _saveSystem.LoadAsync<PlayerProgress>("player_progress");

        //Debug.Log($"Level: {loaded.Level}");
    }
}