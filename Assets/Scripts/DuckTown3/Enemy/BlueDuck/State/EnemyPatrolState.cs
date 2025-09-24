using UnityEngine;


namespace DuckTown3
{
    public class EnemyPatrolState : EnemyGroundState
    {
        //private EnemyController enemy;
        //private Animator animator;
        //private EnemyStateMachine fsm;

        //patrol logic
        //听说要移植到公共控制器领域
        private int currentPatrolIndex = 0;

        public EnemyPatrolState(EnemyController enemy, Animator animator, EnemyStateMachine fsm)
            : base(enemy, animator, fsm)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            animator.Play("BlueDuckWalk");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            SetDestination();
        }

        private void SetDestination()
        {
            Vector3 Des = enemy.patrolPoints[currentPatrolIndex].position;

            if (enemy.Mover.IsAt(Des, enemy.threshold))
            {
                //Debug.Log("is Moving");
                currentPatrolIndex = (currentPatrolIndex + 1) % enemy.patrolPoints.Length;
                Des = enemy.patrolPoints[currentPatrolIndex].position;
                //enemy.Mover.MoveToward(newDes);
                fsm.ChangeState(enemy.GetIdleFor(enemy.Config.restTime, this));
                return;
            }
            //enemy.Mover.MoveToward(Des, enemy.RuntimeData.moveSpeed);

            enemy.Agent.SetDestination(Des);

            // 调用 Mover，里面负责 cc.Move + rotation + 同步
            if (enemy.Agent.hasPath)
            {
                enemy.Mover.MoveToward(enemy.Agent.nextPosition, enemy.RuntimeData.moveSpeed);
            }
        }
    }
}
