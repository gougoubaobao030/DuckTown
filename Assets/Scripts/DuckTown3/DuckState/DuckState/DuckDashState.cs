using System;
using UnityEngine;

public class DuckDashState : DuckStateBase
{
    private float dashTimer = 0.0f;
    private Vector3 DashDir;
    private const float DashVFXReleaseTimerOffset = 0.2f;

    //inject func
    //action vs func
    private Func<GameObject> getDashEffect;

    //用下面那个注入对象池get函数的
    public DuckDashState(DuckControllerV3 duck, DuckStateMachineWithFactory factory) 
        : base(duck, factory)
    {

    }

    public DuckDashState(
        DuckControllerV3 duck,
        DuckStateMachineWithFactory factory,
        Func<GameObject> dashEffect) 
        : base(duck, factory)
    { 
        this.getDashEffect = dashEffect;
    }

    public override void Enter()
    {
        base.Enter();
        SetDebugStateName();
        //唉，不对啊，都new出来吗
        //movementStrategy = new GroundMovementStrategy();

        //做所有初始化清零工作，状态
        dashTimer = 0.0f;

        DashDir = duck.GetInputDirection();
        if (DashDir.magnitude < duck.MoveDeadZone)
        { 
            DashDir = duck.transform.forward;
        }

        PlayDashEffect();
    }

    public override void Exit() 
    { 
        base.Exit();
        duck.LastCameraRotation = duck.cameraController.PlanarRotation();
    }

    public override void Update()
    {
        base.Update();
        
        HandleMovement(DashDir * duck.DashSpeedMultiplier, applyGravity: false);

        dashTimer += Time.deltaTime;
        if (dashTimer >= duck.DashDuration)
        {
            Vector3 inputDir = duck.GetInputDirection();

            if (!duck.isGround)
            {
                factoryStateMachine.ChangeState<DuckFallState>();
                return;
            }

            CheckStateTransitions(inputDir);
        }

       
    }

    private void PlayDashEffect()
    {
        //GameObject dashEffect = GameObject.Instantiate(duck.DashSplashEffectPrefab, duck.transform.position, duck.transform.rotation);
        //仔细回忆特效的时候没有setparent出现了什么...仔细回忆...仔细回忆
        //dashEffect.transform.SetParent(duck.transform);
        //ParticleSystem ps = dashEffect.GetComponentInChildren<ParticleSystem>();

        //仔细一想这好像不需要调整时间啊
        //GameObject.Destroy(dashEffect, duck.DashDuration + 0.2f);

        //object pool

        //写null预防报错中断游戏，例外同样会中断游戏
        if (getDashEffect == null)
        {
            Debug.LogWarning("DashEffect generator not set.");
            return;
        }

        GameObject dashEffect = getDashEffect.Invoke();
        dashEffect.transform.position = duck.transform.position;
        dashEffect.transform.rotation = duck.transform.rotation;
        dashEffect.transform.SetParent(duck.transform);

        PoolRelease dashEffectScript = dashEffect.GetComponent<PoolRelease>();
        dashEffectScript.PlayAndReleaseAfter(duck.DashDuration + DashVFXReleaseTimerOffset);

    }
}
