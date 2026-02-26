using UnityEngine;
using System.Collections.Generic;
using DuckTown3.Quest;
using DuckTown3.Core;
using System.Collections;
using UnityEngine.UI;

namespace DuckTown3.UI
{
    public class UI_QuestPanel : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private GameObject questItemPrefab;

        //readonly 这个list就不能指向别的list了...
        //锁住了引用，不是锁住内容
        private readonly List<UI_QuestItem> questItems = new();
        private readonly Dictionary<string, UI_QuestItem> questMap = new Dictionary<string, UI_QuestItem>();

        private void OnEnable()
        {
            GameEvents.OnQuestListUpdated += RefreshQuestList;
            GameEvents.OnQuestCountUpdated += UpDateQuestItem;
            GameEvents.OnQuestSubmitted += RefreshQuestList;
        }

        private void OnDisable()
        {
            GameEvents.OnQuestListUpdated -= RefreshQuestList;
            GameEvents.OnQuestCountUpdated -= UpDateQuestItem;
            GameEvents.OnQuestSubmitted -= RefreshQuestList;
        }

        private void RefreshQuestList()
        {
            //清空之前显示的，不然会叠加上去
            foreach (var item in questItems)
            {
                Destroy(item.gameObject);
            }
            questItems.Clear();

            //这里只拿了个引用，不伤性能
            List<QuestInstance> quests = QuestManager.Instance.GetQuests;

            for (int i = 0; i < quests.Count; i++)
            {
                if (quests[i].state == QuestState.Submitted) continue;
                GameObject questItem = Instantiate(questItemPrefab, content);
                var script = questItem.GetComponent<UI_QuestItem>();
                script.InitItem(quests[i]);
                questItems.Add(script);
                if (!questMap.ContainsKey(quests[i].data.questID))
                {
                    questMap.Add(quests[i].data.questID, script);
                }

                //Debug.Log("任务UI里的quest数量： " + quests.Count);
            }

            //LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);

            StartCoroutine(RefreshNextFrame());
        }

        private IEnumerator RefreshNextFrame()
        {
            yield return null; //等一帧

            //既然都等一帧了，这行也就不需要了...吧
            LayoutRebuilder.ForceRebuildLayoutImmediate(content as RectTransform);
        }

        private void UpDateQuestItem(string questID)
        {
            if (questMap.ContainsKey(questID))
            {
                var quest = questMap[questID];
                quest.SetItem();
            }
        }
    }
}