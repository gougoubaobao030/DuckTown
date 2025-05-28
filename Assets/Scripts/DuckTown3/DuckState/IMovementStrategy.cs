using UnityEngine;

public interface IMovementStrategy
{
    void Move(Vector3 inputDir, DuckControllerV3 duck, bool applyGravity = true);
}
