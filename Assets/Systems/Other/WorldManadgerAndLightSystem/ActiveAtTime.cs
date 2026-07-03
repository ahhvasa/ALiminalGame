using UnityEngine;

public class ActiveAtTime : MonoBehaviour
{
    public bool activeAtDay;
    public bool activeAtNight;

    public void Start()
    {
        WorldManadger.Instance.OnDayStart += () => SetActive(activeAtDay);
        WorldManadger.Instance.OnNightStart += () => SetActive(activeAtNight);

        bool day = WorldManadger.Instance.stateMachine.Current is WorldDayState;
        if (day) { SetActive(activeAtDay); }
        bool night = WorldManadger.Instance.stateMachine.Current is WorldNightState;
        if (night) { SetActive(activeAtNight); }
    }

    public void SetActive(bool enabled)
    {
        gameObject.SetActive(enabled);
    }
}
