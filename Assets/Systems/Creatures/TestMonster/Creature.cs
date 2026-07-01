using MyLibrary.StateMachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public StateMachine<ICreatureState> stateMachine;

    public void InitializeStateMachine(ICreatureState initialState)
    {
        stateMachine = new StateMachine<ICreatureState>(initialState);
    }

    public void SetIdleState()
    {

    }
}
