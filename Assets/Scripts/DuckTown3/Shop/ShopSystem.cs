using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : ShopBase
{
    public ShopSystem(IShopItemReceiver shopItemReceiver, IGoldSystem goldSystem, List<ShopItemData3> shopItemDataList)
    { 
        this.shopItemReceiver = shopItemReceiver;
        this.goldSystem = goldSystem;
        this.shopItemDataList = shopItemDataList;
        
    }

}
