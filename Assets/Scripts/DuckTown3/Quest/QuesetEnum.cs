using UnityEngine;

namespace DuckTown3.Quest
{
    public enum TaskType
    {
        KillEnemy,
        Harvest
    }

    public enum QuestState { NotAccepted, InProgress, Completed, Submitted }
}