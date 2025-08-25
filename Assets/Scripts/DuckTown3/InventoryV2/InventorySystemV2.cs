using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventorySystemV2 : IInventorySystemV2, IShopItemReceiver, IReceiver
{
    public List<InventoryItemV2> inventoryItems { get; } = new List<InventoryItemV2>();

    public void AddItem(InventoryItemV2 item, int stack = 1)
    {
        if (item.itemData.isStackable)
        { 
            var stackableItem = inventoryItems.FirstOrDefault(i => i.itemData == item.itemData && i.stack < item.itemData.maxStack);

            if (stackableItem != null)
            {
                stackableItem.stack += 1;
                return;
            }
        }

        inventoryItems.Add(item);
    }

    public void RemoveItem(InventoryItemV2 item)
    {
        inventoryItems.Remove(item);
    }

    public void OnShopItemBought(ItemData3 item)
    {
        InventoryItemV2 newitem = new InventoryItemV2(item);
        AddItem(newitem);
    }

    public void ReceiverItem(ItemData3 item)
    {
        InventoryItemV2 newitem = new InventoryItemV2(item);
        AddItem(newitem);
    }
}
