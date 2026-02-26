using UnityEngine;

//最早临时对话系统脚本，
//存在似乎不合理的单例模式
//现在面临淘汰
/// <summary>
/// 新脚本将使用mvc，使用事件相应
/// ui注入只适合特定要做测试的情况 似乎
/// </summary>
public class UI_DialogueManager3 : MonoBehaviour
{
    //All things play their part, and all is well.
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
