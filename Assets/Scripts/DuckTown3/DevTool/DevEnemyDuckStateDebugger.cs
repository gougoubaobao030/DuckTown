using UnityEngine;
using DuckTown3;
public class DevEnemyDuckStateDebugger : MonoBehaviour, IDevToolModule
{
    [SerializeField] private EnemyController blueDuck;

    [SerializeField] private bool showInGUI = true;
    [SerializeField] private bool showInGizmo = true;
    public void DrawGizmos()
    {
        if (showInGizmo == false) return;

        Vector3 origin = blueDuck.transform.position;
        Vector3 forward = new Vector3(blueDuck.transform.forward.x, 0.0f, blueDuck.transform.forward.z);

        //圆形
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin, 3.0f);
        Gizmos.DrawWireSphere(origin, 5.0f);

        //扇形原始直线
        Gizmos.color = Color.red;
        Gizmos.DrawRay(origin + Vector3.up * 0.5f, forward * 6.0f);

        //旋转
        Quaternion left30 = Quaternion.Euler(0, -30.0f, 0);
        Quaternion right30 = Quaternion.Euler(0, 30.0f, 0);
        Gizmos.DrawRay(origin + Vector3.up * 0.5f, left30 * forward * 6.0f);
        Gizmos.DrawRay(origin + Vector3.up * 0.5f, right30 * forward * 6.0f);

    }

    public void DrawGUI()
    {
        if (showInGUI == false) return;

        GUILayout.Label("Current State: " + blueDuck.StateName);

    }
}
