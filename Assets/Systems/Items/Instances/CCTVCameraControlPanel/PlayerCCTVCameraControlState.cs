using System.Collections.Generic;
using UnityEngine;

public class PlayerCCTVCameraControlState : IPlayerState
{
    private Player player;
    private int _currentCameraId = 0;
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

        CameraPoint.Instance.FollowObject(CCTVCameraManager.Instance.cameras[_currentCameraId].itemObject.transform);
    }
    public void OnExit()
    {
        CameraPoint.Instance.FollowPlayer();
    }

    public void FixedUpdate()
    {
    }
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            ExitCameraControlState();
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            CurrentCameraId += 1;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CurrentCameraId -= 1;
        }
    }



    public void ExitCameraControlState()
    {
        player.BaseState();
    }
}

