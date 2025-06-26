using System.Runtime.InteropServices;
using UnityEngine;

public class DuckControllerV3 : MonoBehaviour
{
    [Header("Duck Configuration(Static data)")]
    [SerializeField] private DuckConfigSO duckConfig;
    public DuckConfigSO Config => duckConfig;

    [field: SerializeField] public float DuckMoveSpeed { get; private set; } = 7.0f;
    [field: SerializeField] public float DuckRotationSlerp { get; private set; } = 6.0f;
    [field: SerializeField] public float MoveDeadZone { get; private set; } = 0.1f;

    //先暂时这样写，到时候改成模块化
    [field: SerializeField] public float JumpForce { get; private set; } = 12.0f;
    [field: SerializeField] public float DuckGravity { get; private set; } = -7.0f;

    //dashstate data
    [Header("Dash Data")]
    [field: SerializeField] public float DashDuration { get; private set; } = 0.6f;
    [field: SerializeField] public float DashSpeedMultiplier { get; private set; } = 19.0f;
    //moved to vfxpoolmanager
    //[field: SerializeField] public GameObject DashSplashEffectPrefab { get; private set; }

    [Header("惯性摆烂系数DeepSeek起的名字")]
    [field: SerializeField] public float JumpInertiaInputMultiplier { get; private set; } = 0.6f;
    [field: SerializeField] public float FallInertiaInputMultiplier { get; private set; } = 0.5f;

    [Header("CheckGround Info")]
    //data for checksphere
    [SerializeField] float checkSphereRadius = 0.5f;
    [SerializeField] Vector3 checkSphereOffset;
    [field: SerializeField] public float JumpCheckDelay { get; private set; }
    [SerializeField] LayerMask groundLayer;
    [field: SerializeField] public bool isGround { get; private set; } = false;
    [SerializeField] private float groundCheckDelay = 0.8f;
    private bool isPreviousOnGround = false;
    private float groundStartTime = -1f;
    

    [Header("Runtime State")]
    public float ySpeed = 0f;
    //temp cache test
    [field: SerializeField] public Vector3 LastValidKeyBoardInput { get; private set; } = Vector3.zero;
    //temp cache statebefore jumpstate
    public bool isJumpFromIdleState = false;
    //temp cache test jumpdir
    [field: SerializeField] public Quaternion LastCameraRotation { get; set; }

    [Header("Controller Component")]
    public CharacterController duckCharacterController { get; private set; }
    public CameraControllter cameraController { get; private set; }

    [Header("Input and StateMachine")]
    //statemachine old version
    //public DuckStateMachine duckStateMachine { get; private set; }
    public IDuckInput Input { get; private set; }
    //statemachine factory
    public DuckStateMachineWithFactory duckFactoryStateMachine { get; private set; }

    //debug: 查看状态
    public string DebugCurrentState;

    //module injection
    public IDuckStatusEvent duckStatusEvent { get; private set; }
    public GameObject AfterImagePrefab;

    //闪现层控制 啊真的可以混合
    public LayerMask blinkDodgeObstacleLayer;
    public LayerMask blinkDodgeGroundLayer;

    //伟大的交互系统接拢Time
    public DuckInteractor3 interactManager;
    public bool isInteractStarted { get; set; } = false;
    public bool isInteractEnded { get; set; } = false;
    public IInteractable CurrentInteractTarget;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        //智障说从so复制数据，所以我不想理它。
        //还是理了
        ApplyConfigFromSO();

        duckCharacterController = GetComponent<CharacterController>();
        cameraController = Camera.main.GetComponent<CameraControllter>();

        //get modulue
        duckStatusEvent = GetComponent<DuckStatusEvents>();

        //duckStateMachine = new DuckStateMachine();
        DuckFactoryRegister factory = new DuckFactoryRegister(this);
        duckFactoryStateMachine = new DuckStateMachineWithFactory(factory);
        factory.SetStateMachine(duckFactoryStateMachine);

        Input = new DuckInputDefault();
    }

    private void Start()
    {
        //duckStateMachine.ChangeState(new DuckIdleState(this));
        duckFactoryStateMachine.ChangeState<DuckIdleState>();

        //register interact start/end event
        InteractionEvents.OnInteractionStartWithTarget += InteractionEvents_OnInteractionStartWithTarget;
        InteractionEvents.OnInteractionEnded += InteractionEvents_OnInteractionEnded;
    }

    private void InteractionEvents_OnInteractionStartWithTarget(IInteractable obj)
    {
        CurrentInteractTarget = obj;
        isInteractStarted = true;
    }

    private void InteractionEvents_OnInteractionStarted()
    {
        isInteractStarted = true;
    }

    private void InteractionEvents_OnInteractionEnded()
    {
        isInteractEnded = true;
    }

    private void Update()
    {
        UpdateGroundStatus();
        duckFactoryStateMachine.UpdateState();
    }

    public bool isOnGround()
    {
        //checkshpere你踩到东西了，但不告诉你是什么东西
        //关于transform.TransformPoint(checkSphereOffset)这个，终于懂了
        //checkSphereOffset本身就是local坐标
        //但下面的函数要求穿入一个它的世界坐标
        //如果不转换函数就把它的本地坐标当成世界坐标了
        return Physics.CheckSphere(transform.TransformPoint(checkSphereOffset), checkSphereRadius, groundLayer);
    }

    private void UpdateGroundStatus()
    {
        bool onGroundRes = Physics.CheckSphere(transform.TransformPoint(checkSphereOffset), checkSphereRadius, groundLayer);

        if (isPreviousOnGround != onGroundRes)
        {
            groundStartTime = Time.time;
            //Debug.Log($"落地开始计时：{groundStartTime:F2}");
        }
        else if(onGroundRes == false)
        {
            groundStartTime = -1f;
        }

        isGround = onGroundRes;
        isPreviousOnGround = onGroundRes;
    }

    //封装一个通用延迟判断
    public bool IsStableOnGround => isGround && (Time.time - groundStartTime > groundCheckDelay);

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(225, 0, 225);
        Gizmos.DrawWireSphere(transform.TransformPoint(checkSphereOffset), checkSphereRadius);
    }

    public Vector3 GetInputDirection()
    { 
        Vector3 inputDir = new Vector3(Input.xInput, 0, Input.yInput);

        return inputDir.normalized;
    }

    public void UpdateLastValidInput(Vector3 input)
    {
        if (input.magnitude > MoveDeadZone)
        {
            LastValidKeyBoardInput = new Vector3(input.x, 0, input.z).normalized;
        }
    }

    public void UpdateLastCameraRotation()
    { 
        LastCameraRotation = cameraController.PlanarRotation();
    }

    private void ApplyConfigFromSO()
    { 
        DuckMoveSpeed = duckConfig.DuckMoveSpeed;
        DuckRotationSlerp = duckConfig.DuckRotationSlerp;
        MoveDeadZone = duckConfig.MoveDeadZone;
        JumpForce = duckConfig.JumpForce;
        DuckGravity = duckConfig.DuckGravity;

        DashDuration = duckConfig.DashDuration;
        DashSpeedMultiplier = duckConfig.DashSpeedMultiplier;
        JumpInertiaInputMultiplier = duckConfig.JumpInertiaInputMultiplier;
        FallInertiaInputMultiplier = duckConfig.FallInertiaInputMultiplier;

        checkSphereRadius = duckConfig.checkSphereRadius;
        checkSphereOffset = duckConfig.checkSphereOffset;
        JumpCheckDelay = duckConfig.groundCheckDelay;
        groundLayer = duckConfig.groundLayer;
    }

    public void TriggerInvincibilityStart()
    {
    
    }
}
