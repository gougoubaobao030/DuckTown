using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Editor;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class CameraControllter : MonoBehaviour
{
    [Header("Target & Follow Settings")]
    public Transform CameraFollowPointer;
    public Vector2 framingOffset;
    public float FollowDistance = 5.0f;
    [SerializeField] float minFollowDistance = 4f;
    [SerializeField] float maxFollowDistance = 6f;

    [Header("Camera Movement Speeds")]
    [SerializeField] float posFollowSpeed = 10.0f;
    [SerializeField] float rotFollowSpeed = 20.0f; // 越高越灵敏

    [Header("Rotation Settings")]
    [SerializeField] float cameraRotationSpeed = 1.5f;
    public float maxLimitForVerticalRotation = 45.0f;
    public float minLimitForVerticalRotation = -15.0f;

    [Header("Invert Axis")]
    public bool invertX = false;
    public bool invertY = false;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private float invertXVal => invertX ? -1f : 1f;
    private float invertYVal => invertY ? -1f : 1f;

    void LateUpdate()
    {
        HandleInput();
        UpdateCameraPosition();
    }

    void HandleInput()
    {
        // 缩放滚轮
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        FollowDistance += scroll * -2.0f;
        FollowDistance = Mathf.Clamp(FollowDistance, minFollowDistance, maxFollowDistance);

        // 鼠标旋转输入
        rotationY += Input.GetAxis("Mouse X") * cameraRotationSpeed * invertXVal;
        rotationX += Input.GetAxis("Mouse Y") * cameraRotationSpeed * invertYVal;

        rotationX = Mathf.Clamp(rotationX, minLimitForVerticalRotation, maxLimitForVerticalRotation);
    }

    void UpdateCameraPosition()
    {
        // 焦点位置 + 构图偏移
        Vector3 cameraFocusPosition = CameraFollowPointer.position + new Vector3(framingOffset.x, framingOffset.y, 0);

        // 创建旋转和目标位置
        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        transform.position = cameraFocusPosition - targetRotation * new Vector3(0, 0, FollowDistance);
        transform.rotation = targetRotation;
        
    }

    // 提供给其他脚本使用的平面旋转（不含上下旋转）
    public Quaternion PlanarRotation()
    {
        return Quaternion.Euler(0f, rotationY, 0f);
    }
}
