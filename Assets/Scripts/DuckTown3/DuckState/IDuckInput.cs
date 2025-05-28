using Unity.VisualScripting;
using UnityEngine;

public interface IDuckInput
{
    float xInput { get; }
    float yInput { get; }
    bool isJumpButtomPressed { get; }
    bool isDashButtonPressed { get; }
    bool isBlinkDodgeButtonPressed { get; }
}
