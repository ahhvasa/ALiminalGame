public class SoundSense : CreatureSense
{
    public Sound sound;

    public SoundSense(Sound sound) : base(sound.PerceivableObject, 0, sound.PerceivableObject.transform.position)
    {
        this.sound = sound;
    }

    public override bool EqualInternal(CreatureSense sense)
    {
        try
        {
            SoundSense soundSense = sense as SoundSense;
            return sound == soundSense.sound;
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
