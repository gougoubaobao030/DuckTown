using UnityEditor.Rendering;
using UnityEngine;

public class DuckJumpState : DuckStateBase
{
    //private bool isFromIdleState = false;

    //private IMovementStrategy groundStrategy;
    //private IMovementStrategy airStrategy;

    private bool isFallingByInertia = false;
    private float jumpStartTime;

    public DuckJumpState(DuckControllerV3 duck, DuckStateMachineWithFactory factory) 
        : base(duck, factory)
    {

    }

    public override void Enter()
    {
        base.Enter();

        duck.isJumpFromIdleState = duck.DebugCurrentState == nameof(DuckIdleState);
        //这种多余的添堵变量
        //isFromIdleState = duck.isJumpFromIdleState;

        SetDebugStateName();
        duck.ySpeed = duck.JumpForce;

        //groundStrategy = new GroundMovementStrategy();
        //airStrategy = new AirMovementStrategy();

        movementStrategy = new GroundMovementStrategy();

        jumpStartTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
        duck.LastCameraRotation = duck.cameraController.PlanarRotation();
    }

    public override void Update()
    {
        base.Update();

        Vector3 inputDir = duck.GetInputDirection();
        duck.UpdateLastValidInput(inputDir);
        isFallingByInertia = false;

        if (duck.isJumpFromIdleState)
        {
            if (inputDir.magnitude < duck.MoveDeadZone)
            {
                inputDir = Vector3.zero;
            }
            else
            {
                duck.isJumpFromIdleState = false;
            }
        }
        else if (inputDir.magnitude < duck.MoveDeadZone)
        {
            inputDir = duck.LastValidKeyBoardInput * duck.JumpInertiaInputMultiplier;
            isFallingByInertia = true;
        }
        //Debug.Log(inputDir.magnitude);
        //duck.LastCameraRotation = duck.cameraController.PlanarRotation();
        //if (!isFallingByInertia)
        //{ 
        //duck.LastCameraRotation = duck.cameraController.PlanarRotation();
        //Debug.Log("cache camera");
        //}

        //movementStrategy = isFallingByInertia? airStrategy : groundStrategy;
#if UNITY_EDITOR
        //Debug.Log($"[jumpstate] Using movement strategy: {movementStrategy.GetType().Name}");
        //Debug.Log(duck.LastCameraRotation);
        //Debug.Log("Last Input" + inputDir);
#endif
        HandleMovement(inputDir);


        if (Time.time - jumpStartTime > duck.JumpCheckDelay && duck.isGround)
        {
            CheckStateTransitions(inputDir);
            return;
        }

        if (duck.ySpeed < 0.0f)
        {
            factoryStateMachine.ChangeState<DuckFallState>();
            return;
        }

        // 如果角色已接触地面，并且是从空中落下（非起跳），就尝试转换状态
        //这里交给fall了
        //if (duck.isOnGround() && duck.ySpeed < 0.0f)
        //{ 
            //CheckStateTransitions(inputDir);
            //return;
        //}
    }
}
