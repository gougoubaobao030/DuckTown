using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Color UnSelectedColor, SelectedColor;
    private Image image;


    private void Awake()
    {
        image = GetComponent<Image>();
        UnSelectSlot();
    }

    public void UnSelectSlot()
        
    {
        image.color = UnSelectedColor;
    }

    public void SelectSlot()
    { 
        image.color = SelectedColor;
    }


    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropItem = eventData.pointerDrag;
            DraggableItem draggableItem = dropItem.GetComponent<DraggableItem>();
            draggableItem.parentAfterDrag = transform;
        }
    }
}
