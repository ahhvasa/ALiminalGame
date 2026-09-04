using MyLibrary.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance;

    public StateMachine<IWorldState> stateMachine;

    public WorldDayState worldDayState;
    public WorldNightState worldNightState;

    public event Action OnDayStart;
    public event Action OnNightStart;

    public bool isNightOn = false;

    public void Awake()
    {
        Instance = this;

        worldDayState = new WorldDayState(this);
        worldNightState = new WorldNightState(this);

        stateMachine = new StateMachine<IWorldState>(worldDayState, worldNightState);
        EnterDay();
    }

    public void EnterDay()
    {
        OnDayStart?.Invoke();
        stateMachine.EnterState(worldDayState);
        isNightOn = false;
    }
    public void EnterNight()
    {
        OnNightStart?.Invoke();
        stateMachine.EnterState(worldNightState);
        isNightOn = true;
    }
    public void Win()
    {

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
    public WorldManager worldManadger;

    public WorldDayState(WorldManager worldManadger)
    {
        this.worldManadger = worldManadger;
    }

    public void OnEnter()
    {
        SceneLightManager.Instance.SetDay();
    }
    public void OnExit()
    {

    }

    public void Update()
    {

    }
    public void FixedUpdate()
    {

    }
}

public class WorldNightState : IWorldState
{
    public WorldManager worldManadger;

    public WorldNightState(WorldManager worldManadger)
    {
        this.worldManadger = worldManadger;
    }

    public void OnEnter()
    {
        SceneLightManager.Instance.SetNight();
    }
    public void OnExit()
    {

    }

    public void Update()
    {

    }
    public void FixedUpdate()
    {

    }
}
