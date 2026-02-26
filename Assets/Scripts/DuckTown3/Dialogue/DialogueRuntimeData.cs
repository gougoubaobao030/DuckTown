using UnityEngine;


namespace DuckTown3.Dialogue
{
    public class DialogueRuntimeData
    {
        public DIalogueData DIalogueData;
        public int CurrentIndex;
        public int TotalLines;

        public bool IsLastLine()
        {
            return CurrentIndex == TotalLines - 1;
        }
    }
}
