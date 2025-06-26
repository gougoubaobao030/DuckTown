using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour, IInteractable
{
    private bool isOpened = false;

    public InteractMode InteractMode => InteractMode.OneShot;

    public bool CanInteract()
    {
        throw new System.NotImplementedException();
    }

    public string GetInteractPrompt()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        //todo: isOpened
        GameManager.Instance.GoldSystem.AddGold(10000);
    }
}
