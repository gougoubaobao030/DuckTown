using UnityEngine;

public abstract class DuckStateBase : IDuckState
{
    protected DuckControllerV3 duck;
    protected DuckStateMachineWithFactory factoryStateMachine;

    //move strategy
    //new in specific state
    protected IMovementStrategy movementStrategy;

    protected DuckStateBase(DuckControllerV3 duck, DuckStateMachineWithFactory factory)
    {
        this.duck = duck;
        this.factoryStateMachine = factory;
    }

    public virtual void Enter()
    {       
    }

    public virtual void Exit()
    {     

    }

    public virtual void Update()
    {        
    }

    protected void HandleMovement(Vector3 inputDir, bool applyGravity = true)
    {
        if (movementStrategy == null)
        {
            Debug.LogWarning($"[{this.GetType().Name}] movementStrategy isn't init, use handlegroundmovent default");
            HandleGroundMovement(inputDir, applyGravity);
            return;
        }

        movementStrategy.Move(inputDir, duck, applyGravity);
    }

    //old movement method
    //use it as temp
    private void HandleGroundMovement(Vector3 inputDir, bool applyGravity)
    {
        Vector3 moveDir = duck.cameraController.PlanarRotation() * inputDir;

        if (applyGravity)
        {
            duck.ySpeed += duck.DuckGravity * Time.deltaTime;
        }

        Vector3 velocity = moveDir * duck.DuckMoveSpeed;
        velocity.y = duck.ySpeed;

        duck.duckCharacterController.Move(velocity * Time.deltaTime);

        if (inputDir.magnitude > duck.MoveDeadZone)
        {
            Quaternion rotateTarget = Quaternion.LookRotation(moveDir);
            duck.transform.rotation = Quaternion.Slerp(
                duck.transform.rotation,
                rotateTarget,
                duck.DuckRotationSlerp * Time.deltaTime);
        }

        StickToGround();
    }

    protected void StickToGround()
    {
        if (duck.isGround && duck.ySpeed < 0.0f)
        {
            duck.ySpeed = -0.4f;
        }
    }

    protected void CheckStateTransitions(Vector3 inputDir)
    { 
        if(!duck.isGround) return;

        if (inputDir.magnitude > duck.MoveDeadZone) factoryStateMachine.ChangeState<DuckMoveState>();
        else factoryStateMachine.ChangeState<DuckIdleState>();
    }

    //debug 检查状态
    protected void SetDebugStateName()
    { 
        duck.DebugCurrentState = this.GetType().Name;
    }
}
