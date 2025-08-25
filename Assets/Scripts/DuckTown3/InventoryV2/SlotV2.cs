using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
public class SlotV2 : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private Image frontGround;
    [SerializeField] private TextMeshProUGUI stack;

    public void SetItem(InventoryItemV2 item)
    { 
        frontGround.sprite = item.itemData.icon;

        if (item.itemData.isStackable && item.stack > 1)
        {
            stack.text = item.stack.ToString();
        }
        else
        { 
            stack.text = string.Empty;
        }
    }

    public void Clear()
    { 
        
    }
}
