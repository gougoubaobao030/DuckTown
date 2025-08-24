using UnityEngine;

namespace DuckTown3
{
    public class EnemyRuntimeData
    {
        //Basic Data
        public float moveSpeed;
        public bool pauseAtPatrolPoint;
        public float restTime;
        public float gravity;

        //Chase Data
        public float chaseSpeed;
        public float chaseDistance;
        public float viewRadius;
        public float viewAngle;
        public float attackDistance;

        //Attack Data
        public float cooldownTime;

        public EnemyRuntimeData(EnemyDuckConfig config) 
        {
            RefreshFrom(config);
        }

        public void RefreshFrom(EnemyDuckConfig config)
        { 
            //记录所有需要refresh的data

            moveSpeed = config.moveSpeed;
            pauseAtPatrolPoint = config.pauseAtPatrolPoint;
            restTime = config.restTime;
            gravity = config.gravity;

            chaseSpeed = config.chaseSpeed;
            chaseDistance = config.chaseDistance;
            viewRadius = config.viewRadius;
            viewAngle = config.viewAngle;
            attackDistance = config.attackDistance;

            cooldownTime = config.cooldownTime;

        }
    }
}