using UnityEngine;

public class UI_DialogueManager3 : MonoBehaviour
{
    public static UI_DialogueManager3 instance;

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
        gameObject.SetActive(false);
    }

    public void ShowDialoguePanel()
    { 
        gameObject.SetActive(true);
    }

    public void CloseDialoguePanel()
    {
        gameObject.SetActive(false);
        InteractionEvents.TriggerInteractionEnded();
    }
}
