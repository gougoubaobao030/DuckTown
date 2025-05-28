using UnityEngine;

public class GroundMovementStrategy : IMovementStrategy
{
    public void Move(Vector3 inputDir, DuckControllerV3 duck, bool applyGravity = true)
    {
        Vector3 moveDir = duck.cameraController.PlanarRotation() * inputDir;

        if (applyGravity)
        {
            duck.ySpeed += duck.DuckGravity * Time.deltaTime;
        }

        Vector3 velocity = moveDir * duck.DuckMoveSpeed;
        velocity.y = duck.ySpeed;

        duck.duckCharacterController.Move(velocity * Time.deltaTime);

        if (inputDir.magnitude > duck.MoveDeadZone)
        {
            Quaternion rotateTarget = Quaternion.LookRotation(moveDir);
            duck.transform.rotation = Quaternion.Slerp(
                duck.transform.rotation,
                rotateTarget,
                duck.DuckRotationSlerp * Time.deltaTime);
        }

        //这里我觉得应该要保留的
        if (duck.isGround && duck.ySpeed < 0.0f)
        {
            duck.ySpeed = -0.4f;
        }
    }
}
