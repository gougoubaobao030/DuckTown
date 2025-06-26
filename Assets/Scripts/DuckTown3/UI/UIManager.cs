using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject goldPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private UI_InventoryPanelV2 inventoryPanelV2;

    private UI_ShopPanel shopPanelScript;
    private bool isInventoryOpen = false;

    public void InitShop(IShopSystem3 shopSystem)
    { 
        shopPanelScript = shopPanel.GetComponent<UI_ShopPanel>();
        shopPanelScript.Init(shopSystem);
    }

    public void InitGold(IGoldSystem goldSystem)
    {
        var goldpanelScript = goldPanel.GetComponent<UI_GoldPanel3>();
        goldpanelScript.Init(GameManager.Instance.GoldSystem);
        goldPanel.SetActive(true);
    }

    public void InitInventory(IInventorySystemV2 inventorySystem)
    {
        inventoryPanelV2.Init(inventorySystem);
    }

    public void ShowGoldPanel()
    { 
        goldPanel.SetActive(true);
    }

    public void ShowShopPanel()
    {
        shopPanel.gameObject.SetActive(true);
        shopPanelScript.OpenShop();
    }

    public void HideShopPanel()
    { 
        shopPanel.gameObject.SetActive(false);
        InteractionEvents.TriggerInteractionEnded();
    }

    public void ToggleInventroyPanel()
    { 
        isInventoryOpen = !isInventoryOpen;
        if (isInventoryOpen)
        {
            inventoryPanelV2.OpenInventory();
        }
        else
        {
            inventoryPanelV2.CloseInventory();
        }

    }
}
