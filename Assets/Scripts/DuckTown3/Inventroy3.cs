using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;
using UnityEngine.InputSystem.Editor;

public class Inventroy3 : MonoBehaviour, IShopItemReceiver
{
    public static Inventroy3 instance;

    //make a list for domian model, it means iten3 is raw data
    
    public List<ItemInstance3> itemInstance3s = new List<ItemInstance3>();
    public Dictionary<ItemData3, ItemInstance3> menoOfPickedItems;

    //make equipment list
    public List<ItemInstance3> equipements = new List<ItemInstance3>();
    public Dictionary<ItemData3, ItemInstance3> equipmentedItemCache = new Dictionary<ItemData3, ItemInstance3>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        { 
            Destroy(gameObject);
        }
        //需要外部资源初始化的时候写在这里
        //否则就上面直接new吧
    }

    private void Start()
    {
        menoOfPickedItems = new Dictionary<ItemData3, ItemInstance3>();
    }

    public void EquipmentItem(ItemData3 itemData3)
    { 
        ItemInstance3 item = new ItemInstance3 (itemData3);
        equipements.Add (item);
        equipmentedItemCache.Add(itemData3, item);
    }

    public void AddItem(ItemData3 itemData)
    {

        if(itemData.itemType  != ItemType.Equipment) return;
        if (menoOfPickedItems.TryGetValue(itemData, out ItemInstance3 itemInstance))
        {
            itemInstance.AddStackAmount();
        }
        else
        { 
            //差点搞砸了
            ItemInstance3 newItemInstance = new ItemInstance3(itemData);
            itemInstance3s.Add(newItemInstance);
            menoOfPickedItems.Add(itemData,newItemInstance);
        }
        UI_Inventory3.instance.UpdateUI();
    }

    public void RemoveItem(ItemData3 itemData)
    {
        if (menoOfPickedItems.TryGetValue(itemData, out ItemInstance3 itemInstance))
        {
            if (itemInstance.stackAmount <= 1)
            {
                itemInstance3s.Remove(itemInstance);
                menoOfPickedItems.Remove(itemData);
            }
            else
            { 
                itemInstance.MinusStackAmount();
            }
        }

        UI_Inventory3.instance.UpdateUI();
    }

    public void OnShopItemBought(ItemData3 item)
    {
        throw new System.NotImplementedException();
    }
}
