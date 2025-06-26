using UnityEngine;

public class OneShotInteractCommand : IInteractCommand
{
    public void Excute(IInteractable interactable)
    {
        interactable.Interact();
    }
}
