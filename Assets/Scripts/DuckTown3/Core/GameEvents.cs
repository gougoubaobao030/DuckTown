using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
using DuckTown3.Quest;

namespace DuckTown3.Core
{
    public static class GameEvents
    {
        //public static event Action<int> OnTaskProgress;
        public static event Action<string, int> OnTaskProgress;
        //未来任务更新时的准备
        public static event Action OnQuestListUpdated;
        public static event Action<string> OnQuestCountUpdated;
        public static event Action OnQuestCompleted;
        public static event Action OnQuestSubmitted;

        public static void ReportTaskProgress(string taskID, int count)
        {
            OnTaskProgress?.Invoke(taskID, count);
        }

        public static void QuestListUpdated()
        {
            OnQuestListUpdated?.Invoke();
        }

        public static void QuestCountUpdated(string taskID)
        { 
            OnQuestCountUpdated?.Invoke(taskID);
        }

        public static void QuestCompleted()
        { 
            OnQuestCompleted?.Invoke();
        }

        public static void QuestSubmitted()
        { 
            OnQuestSubmitted?.Invoke();
        }
    }
}
