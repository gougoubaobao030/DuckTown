using UnityEngine;

public class DevSlopeDebugger : MonoBehaviour, IDevToolModule
{
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask layerWantToCheck;
    [SerializeField] private float rayLength = 3.0f;

    [SerializeField] private bool showInScene = true;
    [SerializeField] private bool showInGUI = true;

    private float currentSlope;

    private void Update()
    {
        //it's world space. and finally it will be world space.
        Ray ray = new Ray(target.position + Vector3.up * 1.0f, Vector3.down);
        if (Physics.Raycast(ray, out var hit, rayLength, layerWantToCheck))
        { 
            currentSlope = Vector3.Angle(hit.normal, Vector3.up);
        }
    }

    public void DrawGizmos()
    {
        if(!showInScene || target == null) return;

        Ray ray = new Ray(target.position + Vector3.up * 1.0f, Vector3.down);
        if (Physics.Raycast(ray, out var hit, rayLength, layerWantToCheck))
        {
            float slope = Vector3.Angle(hit.normal, Vector3.up);
            Gizmos.color = slope > 50.0f? Color.red : Color.green;
            Gizmos.DrawRay(hit.point, hit.normal);
            Gizmos.DrawLine(ray.origin, hit.point);

#if UNITY_EDITOR
            UnityEditor.Handles.Label(hit.point + Vector3.up * 0.2f, $"坡度: {slope:F1}°");

#endif
        }
    }

    public void DrawGUI()
    {
        if (!showInGUI) return;
        GUILayout.Label($"[Slope] 当前坡度角: {currentSlope:F1}°");
    }
}
