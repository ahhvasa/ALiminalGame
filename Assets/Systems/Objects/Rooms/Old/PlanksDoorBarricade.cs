using UnityEngine;

public class PlanksDoorBarricade : DoorBarricade
{
    public Item_Planks item;
    public RoomDoor roomDoor;
    public GameObject barricadeOnDoorView;

    public SoundData breakingSound;
    private Sound currentBreakingSound;
    public SoundData breakSound;
    public SoundData installSound;

    public SoundPlayer soundPlayer;

    public override void Break()
    {
        roomDoor.doorBarricade = null;
        barricadeOnDoorView.SetActive(false);

        SoundManager.PlaySound(breakSound, soundPlayer);

        if (currentBreakingSound != null)
        {
            currentBreakingSound.Stop();
            currentBreakingSound = null;
        }
    }

    public override void CancelBreaking()
    {

        if (currentBreakingSound != null)
        {
            currentBreakingSound.Stop();
            currentBreakingSound = null;
        }
    }

    public override void Install()
    {
        barricadeOnDoorView.transform.position = roomDoor.transform.position;
        barricadeOnDoorView.transform.rotation= roomDoor.transform.rotation;
        barricadeOnDoorView.SetActive(true);

        SoundManager.PlaySound(installSound, soundPlayer);
    }

    public override void StartBreaking()
    {
        currentBreakingSound = SoundManager.PlaySound(breakingSound, soundPlayer);
    }
}
