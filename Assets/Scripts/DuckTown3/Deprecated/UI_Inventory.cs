using UnityEngine;

public class UI_Inventory3 : MonoBehaviour
{
    public static UI_Inventory3 instance;
    public Transform parentSlots;
    private UI_InventorySlot3[] uI_Slots;


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
    }

    private void Start()
    {
        uI_Slots = parentSlots.GetComponentsInChildren<UI_InventorySlot3>();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < uI_Slots.Length; i++)
        {
            if (i < Inventroy3.instance.itemInstance3s.Count)
            {
                uI_Slots[i].SetItem(Inventroy3.instance.itemInstance3s[i]);
            }
            else
            {
                uI_Slots[i].ClearSlot();
            }
        }
    }
}
