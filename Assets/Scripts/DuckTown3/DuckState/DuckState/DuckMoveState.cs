using UnityEngine;

public class DuckMoveState : DuckGroundState
{
    public DuckMoveState(DuckControllerV3 duck, DuckStateMachineWithFactory factory)
        :base(duck, factory) 
    { 
    
    }

    public override void Enter()
    {
        base.Enter();
        SetDebugStateName();

        movementStrategy = new GroundMovementStrategy();
        Debug.Assert(movementStrategy != null, "movementStrategy should not be null");
    }

    public override void Exit()
    {
        base.Exit();
    }

    //use ground update();
    //public override void Update()
    //{
        //base.Update();
        //Movement();
        
    //}

    protected override void HandleGroundInput()
    {
        Vector3 inputDir = duck.GetInputDirection();
        duck.UpdateLastValidInput(inputDir);

        if (duck.isGround && duck.Input.isJumpButtomPressed)
        {
            factoryStateMachine.ChangeState<DuckJumpState>();
            return;
        }

        if (!duck.isGround)
        {
            factoryStateMachine.ChangeState<DuckFallState>();
            return;
        }

        if (inputDir.magnitude < duck.MoveDeadZone)
        {
            factoryStateMachine.ChangeState<DuckIdleState>();
            return;
        }

        HandleMovement(inputDir);

        if (duck.Input.isDashButtonPressed)
        {
            factoryStateMachine.ChangeState<DuckDashState>();
            return;
        }
    }


    //没有抽象成base class前的写法，特意保留
    private void Movement()
    {
        var input = duck.Input;

        Vector3 inputDir = new Vector3(input.xInput, 0, input.yInput);

        if (inputDir.magnitude < duck.MoveDeadZone)
        {
            duck.duckFactoryStateMachine.ChangeState<DuckIdleState>();
            return;
        }
        inputDir.Normalize();



        Vector3 moveDir = duck.cameraController.PlanarRotation() * inputDir;
        duck.duckCharacterController.Move(moveDir * duck.DuckMoveSpeed * Time.deltaTime);

        //for rotate
        Quaternion rotateTarget = Quaternion.LookRotation(moveDir);
        duck.transform.rotation = Quaternion.Slerp(duck.transform.rotation, rotateTarget, Time.deltaTime * duck.DuckRotationSlerp);
    }
}
