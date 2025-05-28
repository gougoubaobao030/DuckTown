using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //tow func
    public GameObject draggableItemPrefab;
    public InventorySlot[] InventorySlots;
    private int MaxAmountOfItem = 99;

    private int selectedSlotIndex = -1;

    private void Start()
    {
        SelectedSlotIndexChanged(0);
    }

    private void Update()
    {
        if (Input.inputString != null)
        { 
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number <= 8)
            {
                SelectedSlotIndexChanged(number - 1);
            }
        }
    }

    public void SelectedSlotIndexChanged(int newIndex)
    {
        if (selectedSlotIndex >= 0)
        { 
            InventorySlots[selectedSlotIndex].UnSelectSlot();
        }
        InventorySlots[newIndex].SelectSlot();
        selectedSlotIndex = newIndex;

    }
    public void AddItem(Item item)
    {
        for (int i = 0; i < InventorySlots.Length; ++i)
        { 
            InventorySlot slot = InventorySlots[i];
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();

            if (itemInSlot != null &&
               itemInSlot.item == item &&
               itemInSlot.itemCount < MaxAmountOfItem &&
               itemInSlot.item.stackable)
            {
                itemInSlot.itemCount++;
                itemInSlot.RefreshText();
                return;
            }
        }

        for (int i = 0; i < InventorySlots.Length; ++i)
        { 
            InventorySlot slot = InventorySlots[i];
            //check what?
            DraggableItem itemInSlot = slot.GetComponentInChildren<DraggableItem>();   
            if (itemInSlot == null)
            {
                SpawnSlotItem(item, slot);
                break;
            }
        }

    }

    public void SpawnSlotItem(Item item, InventorySlot slot)
    { 
        //prefab and parent
        GameObject spawnedSlotItem = Instantiate(draggableItemPrefab, slot.transform);
        //PrintComponents(spawnedSlotItem);
        //spawnedSlotItem.SetActive(true);
        DraggableItem newItemScript = spawnedSlotItem.GetComponent<DraggableItem>();
        newItemScript.InitializeItem(item);
    }

    void PrintComponents(GameObject obj)
    {
        Component[] components = obj.GetComponents<Component>();
        Debug.Log($"GameObject {obj.name} 的组件列表:");
        foreach (Component comp in components)
        {
            Debug.Log(comp.GetType().Name);
        }
    }

    public Item GetSelectedSlotItem(bool isUsed)
    {
        if (selectedSlotIndex < 0) return null;

        InventorySlot inventorySlot = InventorySlots[selectedSlotIndex];
        DraggableItem selectedItem = inventorySlot.GetComponentInChildren<DraggableItem>();
        if (selectedItem != null)
        {
            Item item = selectedItem.item;
            if (isUsed)
            {
                selectedItem.itemCount--;
                if (selectedItem.itemCount == 0)
                {
                    Destroy(selectedItem.gameObject);
                }
                else
                { 
                    selectedItem.RefreshText();
                }
            }
            return item;
        }

        return null;
    }


}
