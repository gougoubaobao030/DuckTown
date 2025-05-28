using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UI_InventoryPanelV2 : MonoBehaviour
{
    [SerializeField]private GameObject slotPrefab;

    private ObjectPool<GameObject> slotPool;
    private List<GameObject> activeObjs = new List<GameObject>();

    private IInventorySystemV2 inventorySystem;
    public void Init(IInventorySystemV2 inventorySystem)
    {
        this.inventorySystem = inventorySystem;
        if (slotPool == null)
        {
            //gameObject.SetActive(false);
            slotPool = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(slotPrefab, gameObject.transform),
                actionOnGet: go => go.SetActive(true),
                actionOnRelease: go => go.SetActive(false),
                actionOnDestroy: go => Destroy(go),
                collectionCheck: false,
                defaultCapacity: 30,
                maxSize: 100
            );
        }
        
    }

    private void Start()
    {
        //Debug.Log("还没有初始化");
    }

    public void OpenInventory()
    { 
        gameObject.SetActive(true);
        renderingSlot(inventorySystem.inventoryItems);
        
    }

    public void CloseInventory()
    { 
        gameObject.SetActive(false);
    }

    public void renderingSlot(List<InventoryItemV2> items)
    {
        foreach (var go in activeObjs)
        {
            slotPool.Release(go);
        }
        activeObjs.Clear();

        foreach (var item in items)
        {
            GameObject go = slotPool.Get();
            go.transform.SetAsLastSibling();
            var slotScript = go.GetComponent<SlotV2>();
            slotScript.SetItem(item);
            activeObjs.Add(go);
        }
    }

}
