using UnityEngine;

public class DevInteractSysToState : MonoBehaviour, IDevToolModule
{
    [SerializeField] private DuckControllerV3 duckController;
    [SerializeField] private DuckInteractor3 interactManager;

    [SerializeField] private bool ShowGuI = true;
    [SerializeField] private bool ShowGizmos = true;

    private void Update()
    {
        
    }

    public void DrawGizmos()
    {
        
    }

    public void DrawGUI()
    {
        if(ShowGuI == false) return;

        if (duckController == null)
        {
            Debug.LogWarning("DevTool: DuckController 没拖拽！");
            return;
        }

        if (interactManager == null)
        {
            Debug.LogWarning("DevTool: InteractManager 没拖拽！");
            return;
        }

        GUILayout.Label("DuckController: ");
        GUILayout.Label("duckController.isInteractStarted: " + duckController.isInteractStarted);
        GUILayout.Label("duckController.isInteractEnded: " + duckController.isInteractEnded);
        GUILayout.Label("InteractManager: ");
        GUILayout.Label("interactManager.wasUIVisibleLastFrame: " + interactManager.wasUIVisibleLastFrame);
        GUILayout.Label("interactManager.shouldShowUI: " + interactManager.shouldShowUI);
        GUILayout.Label("interactManager.isInInteractState: " + interactManager.isInInteractState);
    }
}
