using System.Net.NetworkInformation;
using UnityEngine;

public class DevBlinkStateDebugger : MonoBehaviour, IDevToolModule
{
    [SerializeField]private Transform target;
    [SerializeField]private LayerMask obstacleLayer;


    [SerializeField]private bool showInGUI = true;
    [SerializeField]private bool showInGizmo = true;



    public void DrawGizmos()
    {

        if (!showInGizmo || target == null) return;


        Vector3 originPos = target.position;
        Vector3 blinkedPos = target.position + target.forward * 5.0f;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(blinkedPos, 0.1f);


        //目标 画一个检测到的点
        if (Physics.CapsuleCast(originPos + Vector3.up * 0.65f, originPos + Vector3.up * 1.45f, 0.6f, target.forward, out var hit, 5.0f, obstacleLayer))
        {

            Gizmos.color = Color.green;

            Gizmos.DrawSphere(hit.point, 0.1f);

        }


    }

    public void DrawGUI()
    {
        
        if (showInGUI == false) return;

        GUILayout.Label("[Blink Debugger]");

        Vector3 originPos = target.position;
        Vector3 blinkedPos = target.position + target.forward * 5.0f;
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(blinkedPos, 0.1f);

        //bool hashit = Physics.CapsuleCast(originPos + Vector3.up * 0.65f, originPos + Vector3.up * 1.45f, 0.6f, target.forward, out var hit, 5.0f, obstacleLayer);
        //GUILayout.Label("has Hit: " + hashit);

        //目标 画一个检测到的点

    }
}
