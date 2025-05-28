using UnityEngine;

public class AirMovementStrategy : IMovementStrategy
{
    public void Move(Vector3 inputDir, DuckControllerV3 duck, bool applyGravity = true)
    {
        Vector3 moveDir = duck.LastCameraRotation * inputDir;
        //Debug.Log("moveDir" + moveDir);

        if (applyGravity)
        {
            duck.ySpeed += duck.DuckGravity * Time.deltaTime;
        }

        Vector3 velocity = moveDir * duck.DuckMoveSpeed;
        velocity.y = duck.ySpeed;

        duck.duckCharacterController.Move(velocity * Time.deltaTime);
    }
}
