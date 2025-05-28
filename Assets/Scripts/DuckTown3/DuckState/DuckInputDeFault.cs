using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DuckInputDefault : IDuckInput
{
    public float xInput => Input.GetAxis("Horizontal");

    public float yInput => Input.GetAxis("Vertical");

    public bool isJumpButtomPressed => Input.GetKeyDown(KeyCode.Space);

    public bool isDashButtonPressed => Input.GetKeyDown(KeyCode.LeftShift);

    public bool isBlinkDodgeButtonPressed => Input.GetKeyDown(KeyCode.LeftAlt);
}
