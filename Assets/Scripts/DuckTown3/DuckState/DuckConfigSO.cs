using UnityEngine;

//迁移数据的规则，所有可以预设的数据，不是要动态记录的数据
[CreateAssetMenu(fileName = "DuckConfig", menuName = "Duck/Config", order = 0)]
public class DuckConfigSO : ScriptableObject
{
    [Header("move & rotation & jump")]
    [field: SerializeField] public float DuckMoveSpeed { get; private set; } = 7.0f;//
    [field: SerializeField] public float MoveDeadZone { get; private set; } = 0.1f;//
    [field: SerializeField] public float DuckRotationSlerp { get; private set; } = 6.0f;//
    //序列化不支持属性的出现
    //只支持字段的出现
    //最新发现，这只是个状态。
    //public float YSpeed = 0f;//
    [field: SerializeField] public float JumpForce { get; private set; } = 6.5f;//
    [field: SerializeField] public float DuckGravity { get; private set; } = -8.39f;//

    [Header("Dash Data Config")]
    [field: SerializeField] public float DashDuration { get; private set; } = 0.6f;//
    [field: SerializeField] public float DashSpeedMultiplier { get; private set; } = 4.0f;//

    [Header("Jump&Fall Inertia")]
    [field: SerializeField] public float JumpInertiaInputMultiplier { get; private set; } = 0.6f;//
    [field: SerializeField] public float FallInertiaInputMultiplier { get; private set; } = 0.5f;//

    [Header("CheckGround Info")]
    [field: SerializeField] public float checkSphereRadius { get; private set; } = 0.5f;//
    [field: SerializeField] public Vector3 checkSphereOffset { get; private set; } = new Vector3(0.0f, 0.26f, 0.0f);//
    [field: SerializeField] public float groundCheckDelay { get; private set; } = 0.1f;
    [field: SerializeField] public LayerMask groundLayer;
}
