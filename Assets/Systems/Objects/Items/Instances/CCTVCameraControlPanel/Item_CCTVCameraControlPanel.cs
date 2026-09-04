using UnityEngine;
using Zenject.SpaceFighter;

public class Item_CCTVCameraControlPanel : ItemHoldable
{
    public bool active;
    public PlayerCCTVCameraControlState cameraControlState;
    public InputHintInfo hintInfo;

    public SoundData onChooseCamera;

    public Item_CCTVCameraControlPanel()
    {
        hintInfo = new InputHintInfo("F", "See Cameras");
    }

    public override void Activate(bool activateOrDeactivate)
    {
        active = activateOrDeactivate;
        ApplyCameraView(activateOrDeactivate);

        if (active)
        {
            SoundManager.PlaySound(onChooseCamera, playerOwner.soundPlayer);
            InputHintManager.ShowHint(hintInfo);
        }
        else
        {
            InputHintManager.RemoveHint(hintInfo);
        }
    }

    public override void ItemUpdateInternal()
    {
        if (active)
        {
            if (InputProvider.ActivateItem())
            {
                EnterCameraControlState();
            }
        }
    }

    public void EnterCameraControlState()
    {
        cameraControlState = new PlayerCCTVCameraControlState(playerOwner);
        if (playerOwner.playerStateMachine.CurrentState == cameraControlState) { return; }

        playerOwner.EnterState(cameraControlState);
    }


    public void ApplyCameraView(bool activate)
    {
        if (activate)
        {
            playerOwner.roomVision.roomVision.directlyVisibleRooms.AddRange(CCTVCameraManager.GetRoomsSeenByCameras());
        }
        else
        {
            var rooms = CCTVCameraManager.GetRoomsSeenByCameras();
            foreach (var room in rooms)
            {
                if (playerOwner.roomVision.roomVision.directlyVisibleRooms.Contains(room)) { playerOwner.roomVision.roomVision.directlyVisibleRooms.Remove(room); }
            }
        }

    }

}

