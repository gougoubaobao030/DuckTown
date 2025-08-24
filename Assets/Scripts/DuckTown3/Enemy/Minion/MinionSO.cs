using UnityEngine;

namespace DuckTown3
{
    [CreateAssetMenu(fileName = "MinionSO", menuName = "DuckTown3/Enemy/Minion")]
    public class MinionSO : ScriptableObject
    {
        public float chaseRange = 5.0f;
        public float giveUpRange = 7.0f;
        public float attackRange = 2.0f;
        public float respawnDelay = 5.0f;
        public float DistFromSpawn = 25.0f;
    }
}
