using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCCTVCameraControlState : IPlayerState
{
    public InputActionReference interact;
    private Player player;
    private int _currentCameraId = 0;
    private bool _justEntered = false;

    public int CurrentCameraId
    {
        get { return _currentCameraId; }
        set
        {
            _currentCameraId = value;
            if (_currentCameraId >= CCTVCameraManager.Instance.cameras.Count)
            {
                _currentCameraId = 0;
            }
            if (_currentCameraId < 0)
            {
                _currentCameraId = CCTVCameraManager.Instance.cameras.Count - 1;
            }

            CameraPoint.Instance.FollowObject(CCTVCameraManager.Instance.cameras[_currentCameraId].itemObject.transform);
        }
    }


    public PlayerCCTVCameraControlState(Player player)
    {
        this.player = player;
    }

    public void OnEnter()
    {
        Debug.Log("PlayerCCTVCameraControlState");
        player.playerMovement.enabled = false;
        _justEntered = true;

        CameraPoint.Instance.FollowObject(CCTVCameraManager.Instance.cameras[_currentCameraId].itemObject.transform);
    }
    public void OnExit()
    {
        CameraPoint.Instance.FollowPlayer();
    }

    public void FixedUpdate()
    {
        _justEntered = false;
    }
    public void Update()
    {
        if (InputProvider.ActivateItem())
        {
            if (_justEntered) { return; }
            ExitCameraControlState();
        }

        if (InputProvider.MouseScroll(out bool forwardOrBackward))
        {
            CurrentCameraId += forwardOrBackward ? 1 : -1;
        }
    }



    public void ExitCameraControlState()
    {
        player.BaseState();
    }
}

