using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SlotV2 : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private Image frontGround;
    [SerializeField] private TextMeshProUGUI stack;

    public void SetItem(InventoryItemV2 item)
    { 
        frontGround.sprite = item.itemData.icon;
    }
}
