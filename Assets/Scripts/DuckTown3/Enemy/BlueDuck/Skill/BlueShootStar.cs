using UnityEngine;
using DuckTown3.ObjectPool;

namespace DuckTown3.Enemy
{
    [CreateAssetMenu(fileName = "ShootStar", menuName = "DuckTown3/BlueDuck/ShootStar")]
    public class BlueShootStar : BlueDuckSkillSO
    {
        //private Vector3 SkillPointer;
        public override void SkillBeHavior(EnemyController blue)
        {
            Vector3 SkillPointer = blue.SkillPointer.TransformPoint(SkillOffset);

            var star =  ObjectPoolManager.Instance.Get(EffectPrefab);
            star.transform.position = SkillPointer;
            star.transform.rotation = blue.SkillPointer.rotation * Quaternion.Euler(SkillRotateOffset);

            //伤害逻辑，等下再说
        }
    }
}
