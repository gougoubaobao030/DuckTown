using UnityEngine;

public class StateInteractCommand : IInteractCommand
{
    public void Excute(IInteractable interactable)
    {
        //InteractionEvents.TriggerInteractionStarted();
        //Debug.Log("is emit InteractionEvents.TriggerInteractionStarted();");
        InteractionEvents.TriggerInteractionStartWithTarget(interactable);
    }
}
