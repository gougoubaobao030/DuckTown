using UnityEngine;

public class UI_Stash3 : MonoBehaviour
{
    //ui list rendering logic
    public static UI_Stash3 Instance;
    [SerializeField] private Transform StashUIParent;
    [SerializeField] private UI_InventorySlot3[] uiSlots;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        uiSlots = StashUIParent.GetComponentsInChildren<UI_InventorySlot3>();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < StashManager3.instance.stashList.Count; i++)
        {
            uiSlots[i].SetItem(StashManager3.instance.stashList[i]);
        }
    }
}
