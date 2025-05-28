using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class StashManager3 : MonoBehaviour
{
    //itme add logic
    public static StashManager3 instance;
    public List<ItemInstance3> stashList = new List<ItemInstance3>();
    public Dictionary<ItemData3, ItemInstance3> stashCache = new Dictionary<ItemData3, ItemInstance3>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        { 
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    //data driven
    public void AddItem(ItemData3 newItemData)
    {
        if(newItemData.itemType != ItemType.Material) return;
        //这个写法更加优雅
        if (stashCache.TryGetValue(newItemData, out ItemInstance3 value))
        {
            value.AddStackAmount();
        }
        else 
        {
            ItemInstance3 newItemInstance = new ItemInstance3(newItemData);
            stashList.Add(newItemInstance);
            stashCache.Add(newItemData, newItemInstance);
        }
        UI_Stash3.Instance.UpdateUI();
    }

    public void RemoveItem(ItemData3 itemData3)
    {
        if (stashCache.TryGetValue(itemData3, out ItemInstance3 value))
        {
            if (value.stackAmount > 1)
            {
                value.MinusStackAmount();
            }
            else
            { 
                stashList.Remove(value);
                //删除是一般开发时的做法
                stashCache.Remove(itemData3);
            }
        }
        UI_Stash3.Instance.UpdateUI();
    }

}
