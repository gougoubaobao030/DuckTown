using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DuckBlinkDodgeState : DuckStateBase
{
    private float blinkDistance = 5.0f;
    private float blinkDuration = 0.2f;



    public DuckBlinkDodgeState(DuckControllerV3 duck, DuckStateMachineWithFactory factory) 
        : base(duck, factory)
    {

    }

    public override void Enter()
    {
        base.Enter();
        SetDebugStateName();

        //Vector3 inputDir = duck.GetInputDirection();
        //Vector3 dir = inputDir.magnitude > 0.1f ? inputDir : duck.transform.forward;

        //开始状态
        duck.duckStatusEvent.OnDodgeStart();

        Vector3 originPos = duck.transform.position;
        Vector3 newPos = duck.transform.position + duck.transform.forward * blinkDistance;
        //之后要封装起来
        //public void SetPosition(Vector3 pos)
        

        //闪现防止撞墙检测 //根据角色控制器
        //0.7稍微有点坡度也能闪过去
        Vector3 capsuleStart = originPos + Vector3.up * 0.7f;
        Vector3 capsuleEnd = originPos + Vector3.up * 1.45f;
        float capsuleRadius = 0.6f;

        if (Physics.CapsuleCast(capsuleStart, capsuleEnd, capsuleRadius, duck.transform.forward, out var hit, blinkDistance, duck.blinkDodgeObstacleLayer))
        { 
            newPos = hit.point + Vector3.down * 0.65f - 0.62f * duck.transform.forward;
        }

        //实现闪现上坡
        if (Physics.Raycast(newPos + Vector3.up * 1.5f, Vector3.down, out var groundHit, 3f, duck.blinkDodgeGroundLayer))
        {
            newPos = groundHit.point;
        }

        duck.transform.position = newPos;

        GameObject afterImage = Object.Instantiate(duck.AfterImagePrefab, originPos, duck.transform.rotation);

        // 1. 假设你给 BallB 起了个标签或者特定名字
        //Transform body = afterImage.transform.Find("Body"); // 路径名要准确
        //Renderer render_body = body.GetComponent<Renderer>();

        //Transform head = afterImage.transform.Find("Head"); // 路径名要准确
        //Renderer render_head = head.GetComponent<Renderer>();

        // 💥 3. 用 new Material() 创建真正的新材质实例（不是 Instantiate）
        //Material newMat_body = new Material(render_body.sharedMaterial);
        //Material newMat_head = new Material(render_head.sharedMaterial);

        // 🕒 4. 设置 Shader 参数 _SpawnTime
        //newMat_body.SetFloat("_SpawnTime", Time.time);
        //newMat_head.SetFloat("_SpawnTime", Time.time);

        // 👚 5. 把新材质赋回去，这个 Renderer 只用它
        //render_body.material = newMat_body;
        //render_head.material = newMat_head;
        //ReSetShaderTime(afterImage, "Body");
        //ReSetShaderTime(afterImage, "Head");
        string[] partsName = { "Body", "Head" };
        foreach (var name in partsName) ReSetShaderTime(afterImage, name);


        //开始无敌帧
        //duck.TriggerInvincibilityStart();

        duck.StartCoroutine(BlinkDelay());

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    private IEnumerator BlinkDelay()
    { 
        yield return new WaitForSeconds(blinkDuration);
        duck.duckStatusEvent.OnDodgeEnd();
        factoryStateMachine.ChangeState<DuckIdleState>();
    }

    private void ReSetShaderTime(GameObject afterImage, string partsName)
    {
        Transform parts = afterImage.transform.Find(partsName); // 路径名要准确
        Renderer renderer = parts.GetComponent<Renderer>();

        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_SpawnTime", Time.time);
        renderer.SetPropertyBlock(mpb);
    }
}
