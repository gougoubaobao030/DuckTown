using UnityEngine;

public class DuckBlinkDodgeState : DuckStateBase
{
    private float blinkDistance = 5.0f;
    private float blinkDuration = 0.3f;

    public DuckBlinkDodgeState(DuckControllerV3 duck, DuckStateMachineWithFactory factory) 
        : base(duck, factory)
    {

    }

    public override void Enter()
    {
        base.Enter();
        SetDebugStateName();

        Vector3 inputDir = duck.GetInputDirection();
        Vector3 dir = inputDir.magnitude > 0.1f ? inputDir : -duck.transform.forward;

        Vector3 newPos = duck.transform.position + dir * blinkDistance;
        //之后要分装起来
        //public void SetPosition(Vector3 pos)
        duck.transform.position = newPos;

        //开始无敌帧
        duck.TriggerInvincibilityStart();



    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
