using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]public Item item;
    [HideInInspector]public Transform parentAfterDrag;
    [HideInInspector]public int itemCount = 1;

    //image is the part of Ui component
    private TextMeshProUGUI textMeshProUGui;
    private Image image;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("BeginDrag " + transform.root);
        parentAfterDrag = transform.parent;
        //设置父对象，即没有对象的object为对象
        transform.SetParent(transform.root);
        //在同级别里设置在最后
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag.....");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag........");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    private void Awake()
    {
        Debug.Log("it was be called");
    }

    private void Start()
    {
        //image = GetComponent<Image>();

    }

    public void InitializeItem(Item newItem)
    {
        //答案是，拖拽和get都可以
        image = GetComponent<Image>();
        textMeshProUGui = GetComponentInChildren<TextMeshProUGUI>();
        item = newItem;
        //if (image == null)
        //{
        //    Debug.Log("Image is null");
        //}
        image.sprite = item.image;

        //text
        //int count = Random.Range(1, 4);
        RefreshText();
    }

    public void RefreshText()
    { 
    
        textMeshProUGui.text = itemCount.ToString();
    }
}
