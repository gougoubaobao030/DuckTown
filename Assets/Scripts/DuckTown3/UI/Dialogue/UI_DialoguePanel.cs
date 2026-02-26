using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DuckTown3.Dialogue
{
    public class UI_DialoguePanel : MonoBehaviour
    {
        [SerializeField] private GameObject dialoguePanelRoot;
        [SerializeField] private TextMeshProUGUI npcName;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button closeButton;

        [SerializeField] private Image nextButtonIcon;
        [SerializeField] private Sprite nextSprite;
        [SerializeField] private Sprite endSprite;

        private void OnEnable()
        {
            //注册一些事件
            //Dialogue.DialogueManager.Instance.OnTextDisplayed += UpdateDialogueText;
            Dialogue.DialogueManager.Instance.OnDialogueDataUpdated += UpdateDialogueText;
            Dialogue.DialogueManager.Instance.OnDialogueEnded += DialogueEnded;


            nextButton.onClick.AddListener(ShowNextLine);

            //代码给按钮绑定事件
            closeButton.onClick.AddListener(CloseDialogue);

            //Debug.Log("ui_dialoguePanel_onable");
        }
        

        private void OnDisable()
        {
            if (Dialogue.DialogueManager.Instance != null)
            {
                //Dialogue.DialogueManager.Instance.OnTextDisplayed -= UpdateDialogueText;
                Dialogue.DialogueManager.Instance.OnDialogueDataUpdated -= UpdateDialogueText;
                Dialogue.DialogueManager.Instance.OnDialogueEnded -= DialogueEnded;
            }

            closeButton.onClick.RemoveListener(CloseDialogue);
            Debug.Log("ui_dialoguePanel_onDiable");

        }

        private void Start()
        {
            //如果这里再注册一次的话，函数会调用两次
            //Dialogue.DialogueManager.Instance.OnTextDisplayed += UpdateDialogueText;
            //Dialogue.DialogueManager.Instance.OnDialogueEnded += DialogueEnded;

            dialoguePanelRoot.SetActive(false);
        }

        //一些函数用于响应事件
        private void UpdateDialogueText(DialogueRuntimeData obj)
        {
            npcName.text = obj.DIalogueData.npcName;
            dialogueText.text = obj.DIalogueData.lines[obj.CurrentIndex].text;

            //可以做的事，显示进度

            //最后一行的时候改变图标
            //if (obj.IsLastLine())
            //{
            //    nextButtonIcon.sprite = endSprite;
            //}
            //else
            //{
            //    nextButtonIcon.sprite = nextSprite;
            //}
            nextButtonIcon.sprite = obj.IsLastLine() ? endSprite : nextSprite;

            dialoguePanelRoot.SetActive(true);
            //Debug.Log("显示UI面板");
        }

        private void ShowNextLine()
        { 
            Dialogue.DialogueManager.Instance.ShowNextLine();

        }

        private void DialogueEnded()
        {
            //throw new NotImplementedException();
            //分配任务或者跳出别的确认框框
            if (dialoguePanelRoot.activeSelf)
            {
                dialoguePanelRoot.SetActive(false);
                InteractionEvents.TriggerInteractionEnded();
            }
            else
            {
                Debug.Log("CloseDialoguePanel 被调用，但面板已关闭，跳过。");
            }
        }

        private void CloseDialogue()
        {
            //也许还要调用一些结束对话系统的处理
            //

            //gameObject.SetActive(false);
            if (dialoguePanelRoot.activeSelf)
            {
                dialoguePanelRoot.SetActive(false);
                InteractionEvents.TriggerInteractionEnded();
            }
            else
            {
                Debug.Log("CloseDialoguePanel 被调用，但面板已关闭，跳过。");
            }
        }
    }
}