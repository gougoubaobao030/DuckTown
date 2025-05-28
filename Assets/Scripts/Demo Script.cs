using UnityEngine;

public class DemoScript : MonoBehaviour
{
    //不重要，瞎几把搞吧
    public InventoryManager inventoryManager;
    public Item[] pickupItems;

    public void pickupItem(int id)
    {
        inventoryManager.AddItem(pickupItems[id]);
    }
}
