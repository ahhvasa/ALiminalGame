using MyLibrary.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManadger : MonoBehaviour
{
    public static WorldManadger Instance;

    public StateMachine<IWorldState> stateMachine;

    public WorldDayState worldDayState;
    public WorldNightState worldNightState;

    public void Awake()
    {
        Instance = this;

        worldDayState = new WorldDayState(this);
        worldNightState = new WorldNightState(this);

        stateMachine = new StateMachine<IWorldState>(worldDayState, worldNightState);
    }

    public void EnterDay()
    {
        stateMachine.EnterState(worldDayState);
    }
    public void EnterNight()
    {
        stateMachine.EnterState(worldNightState);
    }

    public void Update()
    {
        stateMachine.Current.Update();
    }

    public void FixedUpdate()
    {
        stateMachine.Current.FixedUpdate();
    }

}

public interface IWorldState : IState_UseFrames
{

}

public class WorldDayState : IWorldState
{
    public WorldManadger worldManadger;

    public WorldDayState(WorldManadger worldManadger)
    {
        this.worldManadger = worldManadger;
    }

    public void OnEnter()
    {
        SceneLightManadger.Instance.SetDay();
    }
    public void OnExit()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            worldManadger.EnterNight();
        }
    }
    public void FixedUpdate()
    {

    }
}

public class WorldNightState : IWorldState
{
    public WorldManadger worldManadger;

    public WorldNightState(WorldManadger worldManadger)
    {
        this.worldManadger = worldManadger;
    }

    public void OnEnter()
    {
        SceneLightManadger.Instance.SetNight();
    }
    public void OnExit()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            worldManadger.EnterDay();
        }
    }
    public void FixedUpdate()
    {

    }
}
