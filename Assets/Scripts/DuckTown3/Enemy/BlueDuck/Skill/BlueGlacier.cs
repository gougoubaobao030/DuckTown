using DuckTown3.Duck;
using DuckTown3.ObjectPool;
using DuckTown3.SkillSystemV1;
using UnityEngine;

namespace DuckTown3.Enemy
{
    [CreateAssetMenu(fileName = "Glacier", menuName = "DuckTown3/BlueDuck/Glacier")]
    public class BlueGlacier : BlueDuckSkillSO
    {
        //data
        public float Radius = 4.0f;
        public float Angle = 120.0f;

        private Vector3 SkillPointer;
        private Collider[] hits = new Collider[4];
        public override void SkillBeHavior(EnemyController blue)
        {
            SkillPointer = blue.SkillPointer.TransformPoint(SkillOffset);

            GameObject glacier = ObjectPoolManager.Instance.Get(EffectPrefab);
            glacier.transform.position = SkillPointer;
            glacier.transform.rotation = blue.SkillPointer.rotation * Quaternion.Euler(SkillRotateOffset);

            //伤害逻辑，等下再说
            DetectDuckAndDoDamage(blue);
        }

        private void DetectDuckAndDoDamage(EnemyController blue)
        {
            int hitCounts = Physics.OverlapSphereNonAlloc(blue.transform.position, Radius, hits, targetLayer);

            for (int i = 0; i < hitCounts; i++)
            {
                Collider hit = hits[i];


                Vector3 targetPos = new Vector3(hit.transform.position.x, 0.0f, hit.transform.position.z);
                Vector3 origins = new Vector3(blue.transform.position.x, 0.0f, blue.transform.position.z);

                Vector3 forward = new Vector3(blue.transform.forward.x, 0.0f, blue.transform.forward.z);

                //位置计算错误，等下改

                Vector3 dirToTarget = (targetPos - origins).normalized;

                //Debug.Log("Angle: " + Vector3.Angle(forward, dirToTarget));

                if (Vector3.Angle(forward, dirToTarget) <= (Angle / 2))
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
}
