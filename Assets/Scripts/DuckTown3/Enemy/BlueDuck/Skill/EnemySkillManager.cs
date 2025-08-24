using UnityEngine;
using System.Collections.Generic;

namespace DuckTown3.Enemy
{
    public class EnemySkillManager
    {
        //ject from bludduck controller, not be so bad.
        private List<BlueDuckSkillSO> skillSOs = new();
        //暂时没考虑到意义，应该是有意义的吧
        private float lastUsedTime = -999.0f;
        private BlueDuckSkillSO currentSkillSO;

        //直接生成的时候注入呗，不好吗
        public EnemySkillManager(List<BlueDuckSkillSO> skillsData)
        { 
            skillSOs = skillsData;
        }

        public BlueDuckSkillSO GetAttackSkill()
        {
            return skillSOs[Random.Range(0, skillSOs.Count)];
        }

        //为什么要单独分开调用说实话我还在思考中，但应该有用的吧
        public void RegisterSkillUse(BlueDuckSkillSO skill)
        { 
            currentSkillSO = skill;
        }

        public void UnRegisterSkill()
        { 
            currentSkillSO = null;
        }

        public BlueDuckSkillSO GetComboSkill()
        {
            if (currentSkillSO == null)
            { 
                return null;
            }

            //总觉得要写一写...
            if (currentSkillSO.canCombo == false)
            {
                return null;
            }

            return currentSkillSO.nextSkill;
        }
    }
}
