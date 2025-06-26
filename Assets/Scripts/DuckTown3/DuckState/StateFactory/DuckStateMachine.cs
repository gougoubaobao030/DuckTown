using UnityEngine;

//legacy now
public class DuckStateMachine
{
    public IDuckState currentState { get; private set; }

    public void ChangeState(IDuckState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    { 
        currentState?.Update();
    }
}
