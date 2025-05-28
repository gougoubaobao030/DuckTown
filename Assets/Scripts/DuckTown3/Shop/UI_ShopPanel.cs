using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;

public class UI_ShopPanel : MonoBehaviour
{
    [SerializeField]private GameObject shopItemSlotPrefab;
    [SerializeField]private Transform shopItemContainer;

    private IShopSystem3 shopSystem;
    public Ui_ShopItemSlot currentItemSlot = null;

    private ObjectPool<GameObject> slotPool;
    private List<GameObject> activeSlots = new List<GameObject>();

    public void Init(IShopSystem3 shopSystem)
    { 
        this.shopSystem = shopSystem;

        if (slotPool == null)
        {
            slotPool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(shopItemSlotPrefab, shopItemContainer),
                actionOnGet: go => go.SetActive(true),
                actionOnRelease: go => go.SetActive(false),
                actionOnDestroy: go => Destroy(go),
                collectionCheck: false,
                //没有预热所以也没多大意义
                defaultCapacity: 0,
                maxSize: 30
            );

        }
        
    }

    public void OpenShop()
    {
        RenderShopItem(shopSystem.shopItemDataList);
    }

    private void RenderShopItem(List<ShopItemData3> shopList)
    {

        foreach (var go in activeSlots)
        {
            slotPool.Release(go);
        }
        activeSlots.Clear();

        foreach (ShopItemData3 shopItem in shopList)
        {
            //GameObject itemSlot = Instantiate(shopItemSlotPrefab, shopItemContainer);
            //var slotScript = itemSlot.GetComponent<Ui_ShopItemSlot>();
            //slotScript.SetItem(shopItem, SetCurrentSelectedItem);

            GameObject go = slotPool.Get();
            go.transform.SetAsLastSibling();
            var slotScript = go.GetComponent<Ui_ShopItemSlot>();
            slotScript.SetItem(shopItem, SetCurrentSelectedItem);
            activeSlots.Add(go);

        }
    }

    public void SetCurrentSelectedItem(Ui_ShopItemSlot slot)
    {
        currentItemSlot?.SetSelecteFlag(false);

        if (currentItemSlot == slot)
        {
            currentItemSlot = null;
            return;
        }

        currentItemSlot = slot;
        currentItemSlot.SetSelecteFlag(true);

    }

    //响应购买逻辑
    public void OnBuyButtonClicked()
    {
        string message = $"are you confirm to buy?";

        if (currentItemSlot != null)
        {
            DialogManager.Instance.ShowComfirmMessage(message,
                () => 
                { 
                    shopSystem.TryBuy(currentItemSlot.GetShopItemData()); 
                });
            //shopSystem.TryBuy(currentItemSlot.GetShopItemData());
        }
    }
}
