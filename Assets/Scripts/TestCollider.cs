using UnityEngine;

public class TestCollider : MonoBehaviour
{
    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = (new Vector3(h, 0, v)).normalized;

        
        characterController.Move(moveDir * 3 * Time.deltaTime);
        //反正听说修改position就会传过去。
        //transform.position += moveDir * 3 * Time.deltaTime;
    }
}
