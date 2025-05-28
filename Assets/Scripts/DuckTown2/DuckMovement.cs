using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float rotateSpeed = 360.0f;

    //pramater for check shpere
    [Header("Ground Check Settings")]
    [SerializeField] private Vector3 checkPositionOffset;
    [SerializeField] private float radius = 0.2f;
    [SerializeField] private LayerMask layerMask;

    //get component from maincamera's script
    private CameraDuckController cameraController;
    private Quaternion rotateForward;
    private Animator animator;
    private CharacterController characterController;

    private bool isGround;
    private float ySpeed;

    //attack
    
    private void Awake()
    {
        cameraController = Camera.main.GetComponent<CameraDuckController>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {

    }
    private void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float moveAmonet = Mathf.Clamp01(Mathf.Abs(h) + Mathf.Abs(v));

        Vector3 moveInput = (new Vector3(h, 0, v)).normalized;

        Vector3 moveDir = cameraController.PlanarRotation * moveInput;

        //check is ground;
        CheckGroud();
        if (isGround)
        {
            //不太平坦的地面容易出问题 如果是0.0的话。
            ySpeed = -0.5f;
        }
        else
        {
            ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        var velocity = moveDir * moveSpeed;
        velocity.y = ySpeed;

        //move == tranform.position;
        characterController.Move(velocity * Time.deltaTime);
        //我觉得其实跟forward没有什么特别大的区别。
        //不过这个函数就是特地设计的为了旋转方向用的
        if (moveDir != Vector3.zero)
        {
            rotateForward = Quaternion.LookRotation(moveDir);
        }

        //这个就是让你旋转角色用的
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateForward,
            rotateSpeed * Time.deltaTime);

        //这条代码抄的匆忙，忘记了什么原因（笑）
        //想起来了，是不要突然停下来用的
        animator.SetFloat("moveAmount", moveAmonet, 0.2f, Time.deltaTime);
    }

    private void CheckGroud()
    {

        isGround = Physics.CheckSphere(transform.TransformPoint(checkPositionOffset), radius, layerMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 1, 0.5f);
        Gizmos.DrawSphere(transform.TransformPoint(checkPositionOffset), radius);
    }
}
