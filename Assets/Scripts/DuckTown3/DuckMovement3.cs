using UnityEngine;

public class DuckMovement3 : MonoBehaviour
{
    [SerializeField] float DuckMoveSpeed = 5.0f;
    [SerializeField] float DuckRotationSlerp = 5.0f;
    CharacterController duckCharacterController;
    CameraControllter cameraController;

    [Header("CheckGround Info")]
    //data for checksphere
    [SerializeField] float checkSphereRadius = 0.5f;
    [SerializeField] Vector3 checkSphereOffset;
    [SerializeField] LayerMask groundLayer;

    float ySpeed = 0f;
    [SerializeField] float jumpForce = 12.0f;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        duckCharacterController = GetComponent<CharacterController>();
        cameraController = Camera.main.GetComponent<CameraControllter>();
    }

    private void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        
        Vector3 inputDir = (new Vector3(xInput, 0, zInput)).normalized;
        Vector3 moveDir = cameraController.PlanarRotation() * inputDir;

        bool isGround = isOnGround();

        if (isGround)
        {
            //check到true但离地面还有一段距离的时候就要用这个
            ySpeed = -0.4f;
        }
        else
        {
            //因为是按每秒算的
            ySpeed += (Physics.gravity.y + 2) * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            ySpeed = jumpForce;
        }
        var velocity = moveDir * DuckMoveSpeed;
        velocity.y = ySpeed;

        duckCharacterController.Move(velocity * Time.deltaTime);

        if (inputDir != Vector3.zero)
        {
            //比如相机对向东，我们输入之余世界坐标是东北，实际我们希望转向东南
            var targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, DuckRotationSlerp * Time.deltaTime);
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(225, 0, 225);
        Gizmos.DrawWireSphere(transform.TransformPoint(checkSphereOffset), checkSphereRadius);
    }
}
