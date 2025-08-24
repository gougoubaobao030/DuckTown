using DuckTown3.ObjectPool;
using DuckTown3.TownInput;
using System.Drawing;
using UnityEngine;

namespace DuckTown3.SkillSystemV2
{
    public class SlashMoonSkill : SkillBase<SlashMoonSO>
    {
        private Collider[] colliders = new Collider[10];
        public SlashMoonSkill(SlashMoonSO data, Transform caster, ISkillInputProvider input)
            : base(data, caster, input) 
        { 
        
        }

        public override void BeHavior()
        {
            Vector3 slashPointer = caster.TransformPoint(data.slashPointerOffset);

            GameObject slash = ObjectPoolManager.Instance.Get(data.SlashPrefab);
            slash.transform.position = slashPointer;
            slash.transform.rotation = caster.rotation * Quaternion.Euler(0, data.testAngle, 0);

            MoonSlashCheck();

            TriggerCooldown();
        }

        private void MoonSlashCheck()
        {
            int hitCounts = Physics.OverlapSphereNonAlloc(caster.position, data.attackRadius, colliders, data.enemyLayer);

            for (int i = 0; i < hitCounts; i++)
            {
                Vector3 dir = (colliders[i].transform.position - caster.position).normalized;
                float angle = Vector3.Angle(caster.forward, dir);

                if (angle < data.attackAngle / 2)
                {
                    IAttackable enemy = colliders[i].GetComponent<IAttackable>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage();
                    }
                }
            }
        }
    }
}
