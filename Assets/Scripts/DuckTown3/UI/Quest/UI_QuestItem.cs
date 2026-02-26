using UnityEngine;
using DuckTown3.Quest;
using UnityEngine.UI;
using TMPro;
using DuckTown3.Core;

//用来显示任务栏里的任务
namespace DuckTown3.UI
{
    public class UI_QuestItem : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI questTitle;
        [SerializeField] private TextMeshProUGUI questDescription;
        private string questID;
        private QuestInstance questInstance;

        private void OnEnable()
        {
            //GameEvents.OnQuestCompleted += QuestCompleted;
        }

        private void OnDisable()
        {
            //GameEvents.OnQuestCompleted -= QuestCompleted;
        }

        //改为使用QuestInstance 初始化
        public void InitItem(QuestInstance quest)
        {
            Debug.Assert(quest != null, "QuestInstance 不能为空！");
            questInstance = quest;
            questID = quest.data.questID;
            icon.sprite = quest.data.icon;
            questTitle.text = quest.data.questTitle;
            questDescription.text = quest.data.questDescription;
            questDescription.text += $"({quest.currentCount}/{quest.data.targetCount})";

            
        }

        public void SetItem()
        {
            //Debug.Assert(questInstance != null, "QuestInstance 不能为空！");
            questDescription.text = questInstance.data.questDescription;
            questDescription.text += $"({questInstance.currentCount}/{questInstance.data.targetCount})";

            //似乎update color也要做到这里
            if (questInstance.state == QuestState.Completed)
            {
                UpdateColor();
            }

        }

        private void UpdateColor()
        { 
            
            questDescription.color = Color.green;
            
        }
    }
}
