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
            factoryStateMachine.ChangeState<DuckBlinkDodgeState>();
            return;
        }

        if (duck.isInteractStarted && duck.CurrentInteractTarget != null)
        {
            duck.isInteractStarted = false;
            duck.interactManager.isInInteractState = true;
            factoryStateMachine.ChangeState<DuckInteractState>();
            return;
        }

        HandleGroundInput();
    }

    protected abstract void HandleGroundInput();

}
