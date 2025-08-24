using UnityEngine;

namespace DuckTown3
{
    public class EnemyGroundState : EnemyDuckStateBase<EnemyGroundState>
    {
        public EnemyGroundState(EnemyController enmey, Animator animator, EnemyStateMachine fsm) : base(enmey, animator, fsm)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

#if UNITY_EDITOR
            if (enemy.Config.isEnterChasing == false)
            {   
                //开关在so，关闭防止进入巡逻的状态
                return;
            }
#endif

            if (enemy.Detector.IsPlayerInRange())
            {
                //fsm.ChangeState(enemy.ChaseState);
                fsm.ChangeState(enemy.ChaseState);
            }
        }
    }
}
