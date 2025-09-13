using System.Dynamic;
using UnityEngine;
using DuckTown3.ObjectPool;

public class ItemPickUpInteraction : MonoBehaviour, IInteractable
{
    //public GameObject blashMultiColorPrefab;
    public GameObject blastMultiColor;
    public ItemData3 pickableItem;

    private IReceiver receiver;

    public InteractMode InteractMode => InteractMode.OneShot;

    public void Init(IReceiver receiver)
    { 
        this.receiver = receiver;
    }

    public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }

    public string GetInteractPrompt()
    {
        return "这是一个MilkBall点击勾引辛普森";
    }

    public void Interact()
    {
        receiver.ReceiverItem(pickableItem);
        //Instantiate(blashMultiColorPrefab, transform.position, Quaternion.identity);
        var blast = ObjectPoolManager.Instance.Get(blastMultiColor);
        blast.transform.position = transform.position;
        Destroy(gameObject);
        
    }

}
