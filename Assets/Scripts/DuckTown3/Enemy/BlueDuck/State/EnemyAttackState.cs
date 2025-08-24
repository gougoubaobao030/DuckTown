using DuckTown3.Enemy;
using UnityEngine;

namespace DuckTown3
{
    public class EnemyAttackState : EnemyDuckStateBase<EnemyAttackState>
    {
        private BlueDuckSkillSO selectedSkill;

        public EnemyAttackState(EnemyController enmey, Animator animator, EnemyStateMachine fsm) : base(enmey, animator, fsm)
        {

        }

        public override void Enter()
        {
            base.Enter();
            //enemy.transform.LookAt(enemy.YellowDuck);
            //ExecuteAttackAnim();
            selectedSkill = enemy.SkillManager.GetAttackSkill();
            enemy.SkillExecutor.ExecuteSkill(selectedSkill);
            enemy.SkillManager.RegisterSkillUse(selectedSkill);
        }

        private void ExecuteAttackAnim()
        {
            enemy.Mover.RotateToTarget(enemy.YellowDuck.transform.position, 10.0f);
            animator.Play("BlueDuckHatAttack", 0, 0.0f);
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
            
            Debug.Log("fire! fire! fire! fire!");
            enemy.SkillExecutor.ExecuteSkillHit(selectedSkill);
        }

        public void OnAnimationEvent_AnimEnd()
        {
            //fsm.ChangeState(enemy.CoolDownState);
            if (selectedSkill.canCombo && enemy.SkillManager.GetComboSkill() != null)
            {
                fsm.ChangeState(enemy.ComboState);
            }
            else
            { 
                fsm.ChangeState(enemy.CoolDownState);
                enemy.SkillManager.UnRegisterSkill();
            }
        }
    }
}
