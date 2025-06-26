using UnityEngine;

public class DuckInteractState : DuckStateBase
{
    public DuckInteractState(DuckControllerV3 duck, DuckStateMachineWithFactory factory) : base(duck, factory)
    {

    }

    public override void Enter()
    {
        base.Enter();
        SetDebugStateName();
        duck.CurrentInteractTarget.Interact();
    }

    public override void Exit()
    {
        base.Exit();
        //duck.CurrentInteractTarget = null;
        //duck.interactManager.isInInteractState = false;
        CleanUpInteractionHook();
    }

    public override void Update()
    {
        base.Update();

        //if (duck.isInteractEnded)
        //{ 
        //    duck.isInteractEnded = false;
        //    factoryStateMachine.ChangeState<DuckIdleState>();
        //    return;
        //}
        //
        //if (duck.GetInputDirection().magnitude > duck.MoveDeadZone)
        //{
        //    HandleMovement(duck.GetInputDirection());
        //    if (duck.isInteractEnded)
        //    {
        //        duck.isInteractEnded = false;
        //        factoryStateMachine.ChangeState<DuckIdleState>();
        //        return;
        //    }
        //
        //}

        Vector3 inputDir = duck.GetInputDirection();

        //不光方便调试，以后还可以加各种&&状况
        //和shouldshowUI同一个道理，简直太棒了
        bool isMoving = inputDir.magnitude > duck.MoveDeadZone;
        if (isMoving) HandleMovement(inputDir);

        if (duck.isInteractEnded)
        {
            duck.isInteractEnded = false;
            factoryStateMachine.ChangeState<DuckIdleState>();
            return;
        }
    }

    //说是留一个钩子，不像其他一般抽出函数是为了复用和srp
    //钩子是为了以备不时之需...呃...好吧
    private void CleanUpInteractionHook()
    {
        duck.CurrentInteractTarget = null;
        duck.interactManager.isInInteractState = false;
    }
}
