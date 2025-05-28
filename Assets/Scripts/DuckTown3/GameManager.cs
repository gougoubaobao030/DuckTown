using Unity.VisualScripting;
using UnityEngine;

//迟早改成Zenject注入
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]private Transform pickUpItemRoot;


    public IGoldSystem GoldSystem { get; private set; }
    public IShopSystem3 ShopSystem { get; private set; }
    public IInventorySystemV2 InventorySystem { get; private set; }

    public ShopItemListData3 shopItemDataList;
    public UIManager UIManager;

    [SerializeField] private VFXPoolManager vfxPoolManager;
    public VFXPoolManager VFXPoolManager => vfxPoolManager;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        { 
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Init();
    }

    private void Init()
    {
        InventorySystemV2 InventorySystemImpl = new InventorySystemV2();
        InventorySystem = InventorySystemImpl;

        GoldSystem = new GoldSystem3();
        ShopSystem = new ShopSystem(InventorySystemImpl, GoldSystem, shopItemDataList.shopItemDataList);

        SceneItemInjector(InventorySystemImpl);

        UIManager.InitShop(ShopSystem);
        UIManager.InitGold(GoldSystem);
        UIManager.InitInventory(InventorySystem);
        //这里先暂时不适用初始化，看看情况
        //UIManager.InitInventory();

        //预留管理系统 注册进VisualSystemManager
        vfxPoolManager.InitVFXPool();
    }

    public void SceneItemInjector(IReceiver receiver)
    {
        foreach (Transform child in pickUpItemRoot)
        {
            var itemScript = child.GetComponent<ItemPickUpInteraction>();
            if (itemScript != null)
            {
                itemScript.Init(receiver);
            }
        }
    }
}
