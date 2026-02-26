using DuckTown3.Core;
using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DuckTown3.Quest
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance;

        private List<QuestInstance> quests = new List<QuestInstance>();
        public List<QuestInstance> GetQuests => quests;

        private void Awake()
        {
            if (Instance == null) { Instance = this; }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            GameEvents.OnTaskProgress += QuestExecuted;

        }

        private void OnDisable()
        {
            GameEvents.OnTaskProgress -= QuestExecuted;
        }

        private void Start()
        {
            DuckTown3.Dialogue.DialogueManager.Instance.OnNotAccepntDialogueEnded += AcceptQuest;
            DuckTown3.Dialogue.DialogueManager.Instance.OnSubmitDialogueEnded += SubmitQuest;
        }

        public QuestInstance AddQuest(QuestData data)
        {
            QuestInstance newQuest = new QuestInstance(data);
            newQuest.state = QuestState.NotAccepted;
            quests.Add(newQuest);
            //GameEvents.QuestListUpdated();
            //Debug.Log($"Accept Quest: {data.questTitle}");

            return newQuest;
        }

        private void AcceptQuest(QuestData data)
        {
            //使用Id查，不要用引用查，容易出问题
            QuestInstance currentQuest = quests.FirstOrDefault(d => d.data.questID == data.questID);
            currentQuest.state = QuestState.InProgress;

            GameEvents.QuestListUpdated();
        }

        private void SubmitQuest(QuestData data)
        {
            QuestInstance currentQuest = quests.FirstOrDefault(d => d.data.questID == data.questID);
            currentQuest.state = QuestState.Submitted;

            GameManager.Instance.GoldSystem.AddGold(data.rewardGold);
            GameEvents.QuestSubmitted();
        }

        //轮询 On Enemy Killed
        public void QuestExecuted(string taskID, int count)
        {
            foreach (QuestInstance quest in quests)
            {
                quest.OnProgressUpdate(taskID);
            }
        }
    }

}