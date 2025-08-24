using UnityEngine;

namespace DuckTown3.Enemy
{
    public class EnemySkillExecutor : MonoBehaviour
    {
        private Animator animator;
        private EnemyController enemy;

        public void Init(EnemyController enemy, Animator animator)
        { 
            this.enemy = enemy;
            this.animator = animator;
        }

        public void ExecuteSkill(BlueDuckSkillSO skillData)
        {
            //enemy.Mover.RotateToTarget(enemy.YellowDuck.position, 10.0f);
            animator.Play(skillData.SkillAnimName, 0, 0.0f);
        }

        public void ExecuteSkillHit(BlueDuckSkillSO skillData)
        {
            skillData.SkillBeHavior(enemy);
        }

        //有没有一种可能要生成skill instance呢，应该是要的，但以后再说
    }
}
