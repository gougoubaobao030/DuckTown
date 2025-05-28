using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ShopBase : IShopSystem3
{
    public List<ShopItemData3> shopItemDataList { get; protected set; }
    public event Action<ShopItemData3> OnItemBought;

    protected IShopItemReceiver shopItemReceiver;
    protected IGoldSystem goldSystem;

    public void TryBuy(ShopItemData3 buyItem)
    {
        //去减金币
        goldSystem.SpendGold(buyItem.price);
        //放到背包
        //这个之后再说，老是报错太痛苦了
        shopItemReceiver.OnShopItemBought(buyItem.itemData);
        //通知其他所有该通知的
        OnItemBought?.Invoke(buyItem);
    }

    protected virtual bool CanAfford(ShopItemData3 item)
    { 
        return goldSystem.Gold >= item.price;
    }

    //protected abstract void OnBuySuccess(ShopItemData3 item);

    protected virtual void OnPurchaseFailed(ShopItemData3 item)
    {
        Debug.Log("can't purchase it");
    }
}
