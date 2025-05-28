using UnityEngine;

public abstract class DuckAirState : DuckStateBase
{
    protected DuckAirState(DuckControllerV3 duck, DuckStateMachineWithFactory factory) : base(duck, factory)
    {
    }
}
