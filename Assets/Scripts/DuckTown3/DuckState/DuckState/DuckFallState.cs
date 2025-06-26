using UnityEngine;

public class DuckFallState : DuckStateBase
{
    private IMovementStrategy groundStrategy;
    private IMovementStrategy airStrategy;

    private bool isFallingByInertia = false;
    private float fallStartTime;
    private const float MinFallDuration = 0.19f;
    public DuckFallState(DuckControllerV3 duck, DuckStateMachineWithFactory factory) 
        : base(duck, factory)
    {
        groundStrategy = new GroundMovementStrategy();
        airStrategy = new AirMovementStrategy();
    }

    public override void Enter()
    {
        base.Enter();
        SetDebugStateName();

        fallStartTime = Time.time;
    }

    public override void Exit() { base.Exit(); }

    public override void Update()
    {
        base.Update();
        Vector3 inputDir = duck.GetInputDirection();
        isFallingByInertia = false;
        if (duck.isJumpFromIdleState)
        { 
            inputDir = Vector3.zero;
        }
        else if (inputDir.magnitude < duck.MoveDeadZone)
        {
            inputDir = duck.LastValidKeyBoardInput * duck.FallInertiaInputMultiplier;
            isFallingByInertia = true;
        }

        movementStrategy = isFallingByInertia ? airStrategy : groundStrategy;

#if UNITY_EDITOR
        //Debug.Log($"[FallState] Using movement strategy: {movementStrategy.GetType().Name}");
#endif

        HandleMovement(inputDir);

        //更加显式一点，符合阅读
        //if(!duck.isOnGround()) return;
        //落地后状态切换的选择
        //方便调试
        if (duck.IsStableOnGround)
        {
            //movementStrategy = groundStrategy;
            //HandleMovement(Vector3.zero);
            CheckStateTransitions(inputDir);
            //factoryStateMachine.ChangeState<DuckIdleState>();
            return;
        }
        

        //其实fall状态有个bug
        //就是碰撞卡住的时候
    }
}
