using UnityEngine;

public class Ui_interaction : MonoBehaviour
{
    private IInteractable currentInteractable;

    private void Start()
    {
        gameObject.SetActive(false);
        //InteractionEvents.OnInteractionStarted += Hide;  
    }

    private void OnDisable()
    {
        //InteractionEvents.OnInteractionStarted -= Hide;
    }

    public void Show(IInteractable newInterableObj)
    { 
        currentInteractable = newInterableObj;
        //todo:
        //text, icon...
        gameObject.SetActive (true);
    }

    public void Hide()
    {
        currentInteractable = null;
        gameObject.SetActive(false);
    }
}
