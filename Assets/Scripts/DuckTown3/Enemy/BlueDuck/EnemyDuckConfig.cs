using UnityEngine;


namespace DuckTown3
{
    [CreateAssetMenu(fileName = "DuckEnemySO", menuName = "DuckTown3/Enemy/BlueDuck")]
    public class EnemyDuckConfig : ScriptableObject
    {
        [Header("Base Data")]
        public float moveSpeed = 3.0f;
        public bool pauseAtPatrolPoint = false;
        public float restTime = 4.0f;
        public float gravity = -10.0f;

        [Header("Chase Data")]
        public bool isEnterChasing = true;
        public float chaseSpeed = 6.0f;
        public float chaseDistance = 19.0f;
        public float viewRadius = 10.0f;
        public float viewAngle = 180.0f;
        public float attackDistance = 3.0f;

        [Header("Attack Data")]
        public bool isFanMode = false;
        public float cooldownTime = 2.0f;
    }
}
