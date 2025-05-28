using UnityEngine;
using System.Collections.Generic;

public interface IInventorySystemV2
{
    List<InventoryItemV2> inventoryItems { get; }

    void AddItem(InventoryItemV2 item);
    void RemoveItem(InventoryItemV2 item);
}
