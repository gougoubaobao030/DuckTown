using UnityEngine;
using UnityEngine.XR;

public class DuckStateMachineWithFactory
{
    public IDuckState currentState { get; private set; }
    private DuckFactoryRegister factory;

    public DuckStateMachineWithFactory(DuckFactoryRegister register)
    { 
        this.factory = register;
    }

    public void ChangeState<T>() where T : IDuckState
    { 
        if(currentState is T) return;

        currentState?.Exit();
        currentState = factory.Create<T>();
        currentState.Enter();
    }

    public void UpdateState()
    { 
        currentState?.Update();
    }

}
