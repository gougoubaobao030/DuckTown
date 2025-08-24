using UnityEngine;
using DuckTown3.ObjectPool;
using DuckTown3.Duck;

namespace DuckTown3.Enemy
{
    [CreateAssetMenu(fileName = "GroundSmash", menuName = "DuckTown3/BlueDuck/GroundSmash")]
    public class BlueGroundSmash : BlueDuckSkillSO
    {
        public float SmashRaduis = 5.0f;

        public override void SkillBeHavior(EnemyController blue)
        {
            Vector3 SkillPointer = blue.SkillPointer.TransformPoint(SkillOffset);

            var smoke = ObjectPoolManager.Instance.Get(EffectPrefab);
            smoke.transform.position = SkillPointer;

            //伤害逻辑，等下再说
            DetectDuckAndDamage(blue);
        }

        private void DetectDuckAndDamage(EnemyController blue)
        {
            Collider[] hits = Physics.OverlapSphere(blue.transform.position, SmashRaduis, targetLayer);

            foreach (Collider hit in hits)
            {
                var target = hit.GetComponent<DuckHealth>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }

    }
}