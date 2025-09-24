using UnityEngine;

namespace DuckTown3
{
    public class EnemyChaseState : EnemyDuckStateBase<EnemyChaseState>
    {
        private float attackDistance = 3.0f;

        public EnemyChaseState(EnemyController enmey, Animator animator, EnemyStateMachine fsm) : base(enmey, animator, fsm)
        {

        }

        public override void Enter()
        {
            base.Enter();
            animator.Play("BlueDuckChase");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            float distance = Vector3.Distance(enemy.YellowDuck.position, enemy.transform.position);
            if (distance < attackDistance)
            {
                if (enemy.Config.isFanMode)
                {
                    enemy.Mover.MoveToward(enemy.transform.position, enemy.RuntimeData.chaseSpeed);
                }
                //new for agent
                enemy.StopMoving();

                fsm.ChangeState(enemy.AttackState);
            }
            else
            {
                enemy.SetDestination(enemy.YellowDuck.position);
                if (enemy.Agent.hasPath)
                {
                    //cc
                    //enemy.Mover.MoveToward(enemy.YellowDuck.position, enemy.RuntimeData.chaseSpeed);
                    //agent
                    enemy.Mover.MoveToward(enemy.Agent.nextPosition, enemy.RuntimeData.chaseSpeed);

                }
            }
        }
    }
}
