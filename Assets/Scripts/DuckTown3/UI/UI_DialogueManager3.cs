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
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            InteractionEvents.TriggerInteractionEnded();
        }
        else
        {
            Debug.Log("CloseDialoguePanel 被调用，但面板已关闭，跳过。");
        }
    }
}
