using UnityEngine;

public class DuckIdleState : DuckGroundState
{
    public DuckIdleState(DuckControllerV3 duck, DuckStateMachineWithFactory factory) 
        : base(duck, factory)
    { 
        
    }

    public override void Enter()
    {
        base.Enter();
        SetDebugStateName();
        //todo: animation
    }

    //abstaract to ground
    //public override void Update()
    //{
        //base.Update();
        

    //}

    public override void Exit()
    {
        base.Exit();
        //todo:animation
    }

    protected override void HandleGroundInput()
    {
        //idle不扫尾
        //idle控制进入什么状态
        //进入move状态

        //为了语义美观，结构清晰

        //这里是input的原始数据
        //var input = duck.Input;

        Vector3 inputDir = duck.GetInputDirection();

        if (duck.Input.isJumpButtomPressed && duck.isGround)
        {
            factoryStateMachine.ChangeState<DuckJumpState>();
        }

        //for test 
        bool isMoving = inputDir.magnitude > duck.MoveDeadZone;

        if (isMoving)
        {
            factoryStateMachine.ChangeState<DuckMoveState>();
        }

        StickToGround();
    }
}
