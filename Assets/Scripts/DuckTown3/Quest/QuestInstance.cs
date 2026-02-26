using DuckTown3.Core;
using UnityEngine;

//Quest Instance;
namespace DuckTown3.Quest
{
    //运行时跟踪进度
    [System.Serializable]
    public class QuestInstance
    {
        public QuestData data;
        public int currentCount;
        public QuestState state;
        
        public QuestInstance(QuestData data)
        {
            this.data = data;
            state = QuestState.NotAccepted;
        }

        //这里有疑问，要不要抽象成接口
        public void OnProgressUpdate(string enemyTag)
        {
            //等下来实装
            if (state != QuestState.InProgress) return;
            if (enemyTag == data.targetEnemyTag)
            { 
                currentCount++;
                if (currentCount >= data.targetCount)
                { 
                    state = QuestState.Completed;
                    Debug.Log($"mission complete: {data.questTitle}");
                    GameEvents.QuestCompleted();
                }
                GameEvents.QuestCountUpdated(data.questID);
            }
        }

        //public void OnEnemyKilled(int count)
        //{ 
        //    if(state != QuestState.InProgress) return;
        //    currentCount++;
        //    if (currentCount >= data.targetCount)
        //    { 
        //        state = QuestState.Completed;
        //        Debug.Log($"mission complte: {data.questTitle}");
        //    }
        //}
    }
}