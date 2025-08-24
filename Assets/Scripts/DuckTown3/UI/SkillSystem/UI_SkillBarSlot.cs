using DuckTown3.SkillSystemV2;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

//现在别的都不管，只算setImage
public class UI_SkillBarSlot : MonoBehaviour, 
    IPointerClickHandler,
    IBeginDragHandler,
    IEndDragHandler,
    IDragHandler,
    IDropHandler
{
    [SerializeField]private Image image;
    [SerializeField]private Image cooldownOverlay;
    [SerializeField]private Image globalCDOverlay;

    private System.Action OnImageClicked;
    private UI_Skillbar parentSkillBar;
    private int slotIndex;

    //canvas for drag
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    //trace
    private Transform originalParent;
    private Image ghostImage;

    //cooldown
    private ISkill skill;

    //全局共享变量
    public static UI_SkillBarSlot DraggingSlot { get; private set; }
    public void Init(Canvas canvas, UI_Skillbar skillbar, int i)
    { 
        this.canvas = canvas;
        this.parentSkillBar = skillbar;
        this.slotIndex = i;

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        { 
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void Start()
    {
        //image.sprite = null;
    }

    public void SetSkill(ISkill skill, Sprite icon)
    { 
        this.skill = skill;
        image.sprite = icon;
    }

    public void InjectCallback(System.Action callback)
    { 
        OnImageClicked = callback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("BOOM!");
        OnImageClicked?.Invoke();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BOOM!OnBeginDrag");
        if (image.sprite == null)
        {
            return;
        }

        DraggingSlot = this;

        //记录旧爹
        //originalParent = transform.parent;
        //换个新爹
        //transform.SetParent(canvas.transform);
        ghostImage = new GameObject("GhostImage").AddComponent<Image>();
        ghostImage.transform.SetParent(canvas.transform, false);
        ghostImage.transform.SetAsLastSibling();
        ghostImage.sprite = image.sprite;
        ghostImage.raycastTarget = false;
        ghostImage.color = new Color(1f, 1f, 1f, 0.6f);
        //下面这条不写图片就会变大...变大啊亲，变大。
        ghostImage.rectTransform.sizeDelta = image.rectTransform.sizeDelta;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("BOOM!OnEndDrag");
        DraggingSlot = null;

        if (ghostImage != null)
        { 
            Destroy(ghostImage.gameObject);
            ghostImage = null;
        }

        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("BOOM!OnDrag");
        if (ghostImage != null)
        {
            ghostImage.transform.position = eventData.position;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        var other = DraggingSlot;
        if (other != null && other != this)
        { 
            parentSkillBar.RequestSwap(slotIndex, other.slotIndex);
        }

    }

    private void Update()
    {
        if (skill == null || skill.isOnCoolDown == false)
        {
            cooldownOverlay.fillAmount = 0.0f;
        }
        else
        {
            cooldownOverlay.fillAmount = skill.GetCoolDownPrecent();
        }

        if (skill == null || !parentSkillBar.IsOnGlobalCD())
        {
            globalCDOverlay.fillAmount = 0.0f;
        }
        else
        { 
            globalCDOverlay.fillAmount = parentSkillBar.GetGlobalCooldownPercent();
        }
        
    }

}
