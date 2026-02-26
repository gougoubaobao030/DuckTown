using UnityEngine;

namespace DuckTown3.Quest
{
    [CreateAssetMenu(fileName = "QuestData", menuName = "DuckTown3/QuestData/Quest")]
    public class QuestData : ScriptableObject
    {
        //mission ID
        public string questID;
        public string questTitle;
        [TextArea] public string questDescription;
        public Sprite icon;

        //data
        public TaskType taskType = TaskType.KillEnemy;
        public MinionSO minionData;

        public int targetCount;
        public string targetEnemyTag;
        public int rewardGold;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(questID))
            { 
                //运行时就会生成，非常的厉害。
                questID = System.Guid.NewGuid().ToString();
            }

            if (minionData != null)
            {
                targetEnemyTag = minionData.EnemyName;
            }
            else
            { 
                targetEnemyTag = string.Empty;
            }

        }
    }

}