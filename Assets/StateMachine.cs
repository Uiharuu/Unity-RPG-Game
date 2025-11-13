using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public EntityState currentState { get;private set; }
    public string lastStateName { get; private set; }

    public void Init(EntityState state)
    {
        currentState = state;
        state.Entry();
    }

    public void ChangeState(EntityState newState, string oldStateName = "")
    {
        currentState.Exit();
        currentState = newState;
        currentState.Entry();

        lastStateName = oldStateName;
    }

    public void UpdateActive()
    {
        currentState.Update();
    }
}
