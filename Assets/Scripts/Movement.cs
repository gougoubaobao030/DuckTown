using Unity.Mathematics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotateSpeed = 500.0f;

    private CharacterController characterController;
    private void Awake()
    {
        //id inventory
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (new Vector3(h, 0, v)).normalized;

        //transform.position += moveDir * moveSpeed * Time.deltaTime;
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
        if (moveDir != Vector3.zero)
        {
            Quaternion faceRotate = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, faceRotate, rotateSpeed * Time.deltaTime);
        }
    }
}
