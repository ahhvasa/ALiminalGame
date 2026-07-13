public class SoundSense : CreatureSense
{
    public VisibleObject visibleObject;

    public SoundSense(VisibleObject visibleObject) : base(visibleObject.perceivableObject, 0, visibleObject.perceivableObject.transform.position)
    {
        this.visibleObject = visibleObject;
    }

    public override bool EqualInternal(CreatureSense sense)
    {
        try
        {
            SoundSense soundSense = sense as SoundSense;
            return visibleObject == soundSense.visibleObject;
        }
        catch
        {
            return false;
        }
    }

    public override string ToString()
    {
        return $"[{base.ToString()}] {perceivableObject.gameObject.name} | lastUpdated = {lastTimeUpdated.ToString("F1")} | position = {position}";
    }

    public override void UpdateInternal(CreatureSense sense)
    {

    }
}
