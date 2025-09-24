using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_InventorySlot3 : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public TextMeshProUGUI text;
    public GameObject iconBackground;

    public ItemInstance3 itemInstance;

    private void Start()
    {
        iconBackground.SetActive(false);
    }

    public void SetItem(ItemInstance3 newItemInstance)
    {
        itemInstance = newItemInstance;

        image.color = Color.white;
        iconBackground.SetActive(true);

        if (itemInstance != null)
        {
            image.sprite = itemInstance.itemData.icon;
            if (itemInstance.stackAmount > 1)
            {
                text.text = itemInstance.stackAmount.ToString();
            }
            else
            {
                text.text = string.Empty;
            }
        }
    }

    public void ClearSlot()
    { 
        image.sprite = null;
        text.text = string.Empty;
        iconBackground.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("mouse clicked");
    }
}
