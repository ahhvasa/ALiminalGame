using UnityEngine;

public class ActiveAtTime : MonoBehaviour
{
    public bool activeAtDay;
    public bool activeAtNight;

    public void Start()
    {
        WorldManager.Instance.OnDayStart += () => SetActive(activeAtDay);
        WorldManager.Instance.OnNightStart += () => SetActive(activeAtNight);

        bool day = WorldManager.Instance.stateMachine.Current is WorldDayState;
        if (day) { SetActive(activeAtDay); }
        bool night = WorldManager.Instance.stateMachine.Current is WorldNightState;
        if (night) { SetActive(activeAtNight); }
    }

    public void SetActive(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}
