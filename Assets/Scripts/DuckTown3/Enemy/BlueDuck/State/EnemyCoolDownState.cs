using System.Threading;
using UnityEngine;

namespace DuckTown3
{
    public class EnemyCoolDownState : EnemyDuckStateBase<EnemyCoolDownState>
    {

        //private float cooldownTime = 2.0f;
        private float timer = 0.0f;
        public EnemyCoolDownState(EnemyController enmey, Animator animator, EnemyStateMachine fsm) : base(enmey, animator, fsm)
        {

        }

        public override void Enter()
        {
            base.Enter();
            timer = enemy.RuntimeData.cooldownTime;
            animator.Play("BlueDuckIdle");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (timer > 0.0f)
            { 
                timer -= Time.deltaTime;
                return;
            }

            if (enemy.Detector.IsPlayerInRange())
            {
                fsm.ChangeState(enemy.ChaseState);
            }
            else
            {
                fsm.ChangeState(enemy.GetIdleFor(2.0f, enemy.PatrolState));
            }
        }
    }
}