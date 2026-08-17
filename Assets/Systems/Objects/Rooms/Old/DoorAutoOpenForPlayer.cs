public class DoorAutoOpenForPlayer : OnPlayerNear
{
    public RoomDoor roomDoor;
    public override void Activate(bool playerClose)
    {
        roomDoor.Open(playerClose);

        if (playerClose)
        {
            if (currentPortalSound == null)
            {
                currentPortalSound = SoundManager.PlaySound(portalSound, roomDoor.soundPlayer);
            }
        }
        else
        {
            if (currentPortalSound == null) { return; }
            currentPortalSound.DestroySmoothly();
            currentPortalSound = null;
        }
    }


    private Sound currentPortalSound;

    public SoundData portalSound;

}
