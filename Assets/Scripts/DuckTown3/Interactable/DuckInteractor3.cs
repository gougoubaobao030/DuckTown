using UnityEditor;
using UnityEngine;

public class DuckInteractor3 : MonoBehaviour
{
    //控制模块开关
    [Header("Gizmos Switch")]
    [SerializeField] bool showInteractableRangeGizmo = true;

    [SerializeField] float checkRadius = 5.0f;
    [SerializeField] LayerMask isInteractable;

    //是否在交互状态的bool值，会做入接口
    public bool isInInteractState { get; set; } = false;

    public Ui_interaction ui_Interaction;

    IInteractable interactable;

    Collider[] noAllcolliders = new Collider[10];

    //state caching
    public bool wasUIVisibleLastFrame = false;
    public bool shouldShowUI = false;

    //use command route behavior.
    InteractionCommandRouter commandRounter;

    private void Start()
    {
        commandRounter = new InteractionCommandRouter();
        commandRounter.Register(InteractMode.OneShot, new OneShotInteractCommand());
        commandRounter.Register(InteractMode.State, new StateInteractCommand());
    }

    private void Update()
    {

        //Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius, isInteractable);
        //GC
        int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, checkRadius, noAllcolliders, isInteractable);
        float closestDistance = Mathf.Infinity;
        interactable = null;

        for (int i = 0; i < colliderCount; i++)
        {
            IInteractable someInteractable = noAllcolliders[i].GetComponent<IInteractable>();
            if (someInteractable != null)
            {
                float distanceToDuck = Vector3.Distance(transform.position, noAllcolliders[i].transform.position);
                if (distanceToDuck < closestDistance)
                {
                    closestDistance = distanceToDuck;
                    interactable = someInteractable;
                }
            }
        }

        //旧的UI刷新状态
        //show ui state
        //bool hasTarget = (interactable != null);
        //
        //if (hasTarget != wasUIVisibleLastFrame)
        //{
        //    if (hasTarget && isInInteractState == false)
        //    {
        //        ui_Interaction.Show(interactable);
        //    }
        //    else
        //    {
        //        ui_Interaction.Hide();
        //    }
        //    wasUIVisibleLastFrame = hasTarget;
        //}

        //新的UI显示判定方法
        UpdateInteractionUI();

        //Debug.Log(interactable.GetInteractPrompt());
        if (Input.GetKeyDown(KeyCode.E))
        {
            //version 1.
            //TryInteract();
            //version 2.
            //InteractionEvents.TriggerInteractionStarted();
            //version 3.
            //var mode = interactable.InteractMode;
            //Debug.Log("没想到这么快就用到了： " + mode);
            //commandRounter.ExcuteCommand(mode, interactable);
            //version 4.
            TryExcuteInteract();
        }
    }

    //use for button click
    public void TryExcuteInteract()
    {
        if (interactable == null)
        {
            Debug.LogWarning("交互失败：没有交互目标");
            return;
        }

        if (isInInteractState)
        {
            Debug.LogWarning("当前正在交互中，忽略重复交互请求");
            return;
        }

        var mode = interactable.InteractMode;
        Debug.Log("瞬时交互或者状态交互： " + mode);
        commandRounter.ExcuteCommand(mode, interactable);

    }

    //在DuckInteractState里调用会存在
    //数据来源不一致问题
    //且控制层和执行层应该有区分
    public void TryInteract()
    {
        if (interactable != null)
        { 
            interactable.Interact();
        }
    }

    private void UpdateInteractionUI()
    { 
        //从各种条件判断是否应该显示UI
        shouldShowUI = (interactable != null) && isInInteractState == false;

        //仅处理上一帧UI是否显示了的逻辑，不考虑其他的。
        if (shouldShowUI != wasUIVisibleLastFrame)
        {
            if (shouldShowUI)
            {
                ui_Interaction.Show(interactable);
            }
            else
            {
                ui_Interaction.Hide();
            }
            wasUIVisibleLastFrame = shouldShowUI;
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (!showInteractableRangeGizmo) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
