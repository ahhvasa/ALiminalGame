using UnityEngine;

public abstract class DoorBarricade : MonoBehaviour 
{
    public void Activate(bool installOrBreak)
    {
        if (installOrBreak)
        {
            Install();
        }
        else
        {
            Break();
        }
    }
    public abstract void Install();
    public abstract void Break();
    public float doorBarricadeDurability = 1;

    public abstract void StartBreaking();
    public abstract void CancelBreaking();

    public float GetBreakingTime(float breakingPower)
    {
        return doorBarricadeDurability / breakingPower;
    }
}
