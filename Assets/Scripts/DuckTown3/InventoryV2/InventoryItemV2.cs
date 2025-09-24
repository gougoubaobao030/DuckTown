using UnityEngine;

//这个相当于Instance实例
public class InventoryItemV2
{
    public ItemData3 itemData;
    //为什么是实例管呢
    //答：因为这个是实时的
    public int stack;

    public InventoryItemV2(ItemData3 itemData3, int stack = 1)
    { 
        this.itemData = itemData3;
        this.stack = stack;
    }
}
