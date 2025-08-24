using UnityEngine;

namespace DuckTown3.Enemy
{
    public class EnemyComboState : EnemyDuckStateBase<EnemyComboState>
    {

        private BlueDuckSkillSO comboSkill;

        public EnemyComboState(EnemyController enmey, Animator animator, EnemyStateMachine fsm) : base(enmey, animator, fsm)
        {
        }

        public override void Enter()
        {
            base.Enter();
            comboSkill = enemy.SkillManager.GetComboSkill();

            if (comboSkill == null)
            {
                //总觉得不是idle
                fsm.ChangeState(enemy.CoolDownState);
                return;
            }

            enemy.SkillExecutor.ExecuteSkill(comboSkill);
            enemy.SkillManager.RegisterSkillUse(comboSkill);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            enemy.Mover.RotateToTarget(enemy.YellowDuck.position, 10.0f);
        }

        public void OnAnimationEvent_Hit()
        {
            // 命中逻辑
            enemy.SkillExecutor.ExecuteSkillHit(comboSkill);
        }

        public void OnAnimationEvent_AnimEnd()
        {
            Debug.Log("Animation is End");
            if (comboSkill.canCombo && comboSkill.nextSkill != null)
            {
                Debug.Log("will go to combo");
                fsm.ChangeState(enemy.ComboState);
                
            }
            else
            {
                fsm.ChangeState(enemy.CoolDownState);
                enemy.SkillManager.UnRegisterSkill();
                Debug.Log("will go to cooldown");

            }
        }
    }
}
