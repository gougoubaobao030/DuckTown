using DuckTown3.TownInput;
using UnityEngine;
using DuckTown3.ObjectPool;


namespace DuckTown3.SkillSystemV2
{
    public class ColdBladeMoonSkill : SkillBase<ColdBladeMoonSO>
    {

        public ColdBladeMoonSkill(ColdBladeMoonSO data, Transform caster, ISkillInputProvider input) 
            : base(data, caster, input)
        {

        }

        public override void BeHavior()
        {
            Vector3 spawnPointer = caster.TransformPoint(data.spawnOffset);
            GameObject coldMoon = ObjectPoolManager.Instance.Get(data.ColdMoonPrefab);
            coldMoon.transform.position = spawnPointer;
            coldMoon.transform.rotation = caster.rotation * Quaternion.Euler(0, 0, data.rotZ);

            //this script is in SkillSystemV1/ColdMoonBladeProjectile3
            var projectile = coldMoon.GetComponent<ColdMoonBladeProjectile3>();
            if (projectile != null)
            {
                projectile.Init(data.maxFlyDistance, data.flySpeed);
            }

            TriggerCooldown();
        }
    }
}
