using UnityEngine;

public class InventoryItemV2
{
    public ItemData3 itemData;
    public int stack;

    public InventoryItemV2(ItemData3 itemData3, int stack = 1)
    { 
        this.itemData = itemData3;
        this.stack = stack;
    }
}
