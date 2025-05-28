using System;
using System.Collections.Generic;
using UnityEngine;

public interface IShopSystem3
{
    List<ShopItemData3> shopItemDataList { get; }
    event Action<ShopItemData3> OnItemBought;
    void TryBuy(ShopItemData3 buyItem);

}
