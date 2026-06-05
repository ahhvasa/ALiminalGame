using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState _currentState;
    public Player player;
    public PlayerBaseState playerBaseState;
    public PlayerDeadState playerDeadState;

    public void Awake()
    {
        playerBaseState = new PlayerBaseState(player);
        playerDeadState = new PlayerDeadState(player);
        _currentState = playerBaseState;
        _currentState.OnEnter();
    }

    public void FixedUpdate()
    {
        _currentState.FixedUpdate();
    }

    public void Update()
    {
        _currentState.Update();
    }

    public void EnterState(IPlayerState state)
    {
        _currentState.OnExit();
        _currentState = state;
        _currentState.OnEnter();
    }
}
