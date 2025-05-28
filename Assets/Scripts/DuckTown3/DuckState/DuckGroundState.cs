using UnityEngine;

public abstract class DuckGroundState : DuckStateBase
{
    protected DuckGroundState(DuckControllerV3 duck, DuckStateMachineWithFactory factory) 
        : base(duck, factory)
    {

    }

    public override void Update()
    {
        base.Update();
        if (duck.Input.isBlinkDodgeButtonPressed && duck.isGround)
        { 
            //change to blink state
        }

        HandleGroundInput();
    }

    protected abstract void HandleGroundInput();

}
