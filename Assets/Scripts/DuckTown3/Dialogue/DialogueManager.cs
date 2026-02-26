using DuckTown3.Quest;
using System;
using UnityEngine;

namespace DuckTown3.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        private static DialogueManager instance;

        //使用懒加载 避免unity初始化顺序的影响
        public static DialogueManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<DuckTown3.Dialogue.DialogueManager>();
                    if (instance == null)
                    {
                        Debug.LogError("场景里找不到debugManager");
                    }
                }

                return instance;
            }
        }

        //dialogue data
        //private DIalogueData currentDialogue;
        //private int currentIndex = -1;

        //runtimeData
        private DialogueRuntimeData DialogueInstance;

        //event to UI_Panel;
        //使用事件通知而不是UIPanel注入实现完全解耦
        //public event Action<DIalogueData> OnTextDisplayed;
        public event Action OnDialogueEnded;

        //runtimeDataEvent
        public event Action<DialogueRuntimeData> OnDialogueDataUpdated;

        //为了实现没接受任务的时候说完话接受任务写的功能，写完再思考和什么合并
        public event Action<QuestData> OnNotAccepntDialogueEnded;
        public event Action<QuestData> OnSubmitDialogueEnded;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void StartTalk(DIalogueData data)
        {
            if (data == null) return;
            //currentDialogue = data;

            DialogueInstance = new DialogueRuntimeData
            {
                DIalogueData = data,
                CurrentIndex = 0,
                TotalLines = data.lines.Count
            };

            //invoke
            //OnTextDisplayed?.Invoke(currentDialogue);
            //OnDialogueDataUpdated?.Invoke(DialogueInstance);
            RaiseCurrentLine();
        }

        //func shownextLine
        //作用 增加index
        //管理是否显示对话或者结束对话

        public void ShowNextLine()
        { 
            if(DialogueInstance == null) return;

            DialogueInstance.CurrentIndex++;

            if (DialogueInstance.CurrentIndex < DialogueInstance.TotalLines)
            {
                //func
                //输出当前对话内容
                RaiseCurrentLine();
            }
            else 
            {
                if (DialogueInstance.DIalogueData.logueType == DiaLogueType.QuestNotAccept)
                {
                    OnNotAccepntDialogueEnded?.Invoke(DialogueInstance.DIalogueData.startQuest);
                }

                if (DialogueInstance.DIalogueData.logueType == DiaLogueType.QuestSubmitted)
                {
                    OnSubmitDialogueEnded?.Invoke(DialogueInstance.DIalogueData.startQuest);
                }
                //对话到最后，执行对话已经到最后的相关操作
                EndTalk();

            }

        }

        //raise == 触发
        private void RaiseCurrentLine()
        {
            OnDialogueDataUpdated?.Invoke(DialogueInstance);
        }

        public void EndTalk()
        {
            DialogueInstance = null;
            OnDialogueEnded?.Invoke();
        }
    }
}