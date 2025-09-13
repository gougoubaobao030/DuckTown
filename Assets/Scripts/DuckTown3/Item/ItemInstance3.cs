using UnityEngine;

//it's domain model
[System.Serializable]
public class ItemInstance3
{
    public ItemData3 itemData;
    public int stackAmount = 1;

    //运行时状态（可以不实现，但要预留）
    //public int durability = -1;    // 耐久（装备/可破坏物品）
    //public bool isEquipped = false;// 是否已装备（装备类物品）

    //强化/成长相关
    //public int upgradeLevel = 0;   // 强化等级（魂类武器系统）
    //public int experience = 0;     // 经验值（灵魂武器成长系统）

    //绑定状态
    public bool isBound = false;   // 是否绑定（不可交易）

    //时间与来源
    //public System.DateTime acquiredTime; // 获得时间（成就/排序/存档用）
    //public string source = "";           // 来源说明（任务/掉落/商店）

    //唯一标识（非必要，但大型系统推荐）
    //public string instanceID;      // 可用于存档/查找特定物品

    public ItemInstance3(ItemData3 item) 
    {
        this.itemData = item;
    }

    public void AddStackAmount() => stackAmount++;

    public void MinusStackAmount() => stackAmount--;
}
