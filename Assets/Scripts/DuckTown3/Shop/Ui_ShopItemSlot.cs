using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ui_ShopItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]private Image itemImage;
    [SerializeField]private TextMeshProUGUI priceText;
    [SerializeField]private Image ItemBackground;
    [SerializeField]private Color newColor;

    private ShopItemData3 itemdata;
    public bool isSelected = false;
    //现在改成事件机制
    //更新：这里恐怕是一个委托
    public Action<Ui_ShopItemSlot> OnShopItemSelected;

    private Color imageColor;

    private void Start()
    {
        imageColor = ItemBackground.color;
    }

    //getter
    public bool GetSelecteFlag() => isSelected;

    //setter
    public void SetSelecteFlag(bool isSelected)
    { 
        this.isSelected = isSelected;
        ItemBackground.color = this.isSelected?  newColor : imageColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnShopItemSelected?.Invoke(this);
    }

    public void SetItem(ShopItemData3 itemInfo, Action<Ui_ShopItemSlot> clickCallBack)
    { 
        itemdata = itemInfo;
        itemImage.sprite = itemInfo.itemData.icon;
        priceText.text = itemInfo.price.ToString();
        OnShopItemSelected = clickCallBack;
    }

    public ShopItemData3 GetShopItemData() => itemdata;
}
