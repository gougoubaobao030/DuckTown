using UnityEngine;

public interface IShopItemReceiver
{
    //备注：我不知道是传shopitem还是item还是留给inventoryItem
    void OnShopItemBought(ItemData3 item);
}
