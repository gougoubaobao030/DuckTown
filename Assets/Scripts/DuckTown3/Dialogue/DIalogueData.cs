using UnityEngine;
using System.Collections.Generic;
using DuckTown3.Quest;

namespace DuckTown3.Dialogue
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "DuckTown3/DialogueData/dialogue")]
    public class DIalogueData : ScriptableObject
    {
        public string dialogueID;
        public string npcName;
        //这里不初始化就会是null
        public List<DialogueLine> lines = new();

        public QuestData startQuest;
        public QuestData endQuest;

        public DiaLogueType logueType;

        [System.Serializable]
        public class DialogueLine
        { 
            [TextArea(3, 10)]
            public string text;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(dialogueID))
            {
                dialogueID = System.Guid.NewGuid().ToString();
            }
        }
#endif
    }

    public enum DiaLogueType {QuestNotAccept, QuestInProgeree, QuestCompleted, QuestSubmitted, SmallTalk};
}