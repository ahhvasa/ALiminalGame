using UnityEngine;
using Zenject.SpaceFighter;

public class Item_CCTVCameraControlPanel : ItemHoldable
{
    public bool active;
    public PlayerCCTVCameraControlState cameraControlState;

    public override void Activate(bool activateOrDeactivate)
    {
        active = activateOrDeactivate;
        ApplyCameraView(activateOrDeactivate);
    }

    public override void ItemUpdateInternal()
    {
        if (active)
        {
            if (Input.GetKeyDown(KeyCode.F))
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
            playerOwner.roomSeer.directlyVisibleRooms.AddRange(CCTVCameraManager.GetRoomsSeenByCameras());
        }
        else
        {
            var rooms = CCTVCameraManager.GetRoomsSeenByCameras();
            foreach (var room in rooms)
            {
                if (playerOwner.roomSeer.directlyVisibleRooms.Contains(room)) { playerOwner.roomSeer.directlyVisibleRooms.Remove(room); }
            }
        }

    }

}

