using UnityEngine;

public class PlanksDoorBarricade : DoorBarricade
{
    public GameObject barricadeInWorld;
    public GameObject barricadeOnDoorView;

    public GameObject[] carricadeInWorldSpawnPointOnBreak;

    public SoundData breakingSound;
    private Sound currentBreakingSound;
    public SoundData breakSound;
    public SoundData installSound;

    public SoundPlayer soundPlayer;

    public GameObject gameObjectOnBreakign;

    public override void Break()
    {
        roomDoor.doorBarricade = null;
        roomDoor.isLockedByBarricade = false;

        barricadeOnDoorView.SetActive(false);
        barricadeInWorld.SetActive(true);

        barricadeInWorld.transform.position = carricadeInWorldSpawnPointOnBreak[UnityEngine.Random.Range(0, carricadeInWorldSpawnPointOnBreak.Length)].transform.position;

        SoundManager.PlaySound(breakSound, soundPlayer);

        if (currentBreakingSound != null)
        {
            currentBreakingSound.DestroySound();
            currentBreakingSound = null;
        }
        gameObjectOnBreakign.SetActive(false);
    }

    public override void CancelBreaking()
    {

        if (currentBreakingSound != null)
        {
            currentBreakingSound.DestroySound();
            currentBreakingSound = null;
        }
        gameObjectOnBreakign.SetActive(false);
    }

    public override void Install(RoomDoor roomdoor)
    {
        item.playerOwner.playerInventory.DropItem();

        roomDoor = roomdoor;
        roomDoor.doorBarricade = this;
        roomDoor.isLockedByBarricade = true;

        barricadeOnDoorView.transform.position = roomDoor.transform.position;
        barricadeOnDoorView.transform.rotation= roomDoor.transform.rotation;
        barricadeOnDoorView.SetActive(true);
        barricadeInWorld.SetActive(false);

        SoundManager.PlaySound(installSound, soundPlayer);
    }

    public override void StartBreaking()
    {
        currentBreakingSound = SoundManager.PlaySound(breakingSound, soundPlayer);
        gameObjectOnBreakign.SetActive(true);
    }
}
