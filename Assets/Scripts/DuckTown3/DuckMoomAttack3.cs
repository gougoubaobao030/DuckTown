using UnityEngine;

public class DuckMoonAttack3 : MonoBehaviour
{
    public float attackRadius = 3f;
    public float attackAngle = 120f; // 扇形角度
    public LayerMask enemyLayer;

    public GameObject slashEffectPrefab; // 拖进来你的半月弯Prefab
    public Transform slashSpawnPoint;    // 特效出现的位置（比如鸭鸭的嘴前方）


    //gizoms swith
    public bool drawGizmo = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        //Debug.Log("perform perfomattac");
        //Debug.Log("slashEffectPrefab " + (slashEffectPrefab == null) );
        //Debug.Log("slashSpawnPoint " + (slashSpawnPoint == null));

        if (slashEffectPrefab != null && slashSpawnPoint != null)
        {
            GameObject effect = Instantiate(slashEffectPrefab, slashSpawnPoint.position, transform.rotation);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            
            if (ps != null)
            {
                ps.Play();
            }
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, enemyLayer);

        foreach (Collider hit in hits)
        {
            Vector3 dirToTarget = (hit.transform.position - transform.position).normalized;
            float angleToTarget = Vector3.Angle(transform.forward, dirToTarget);

            if (angleToTarget <= attackAngle / 2f)
            {
                IAttackable attackable = hit.GetComponent<IAttackable>();
                if (attackable != null)
                {
                    attackable.TakeDamage();//一斩击飞
                }
            }
        }
    }

    // 可视化扇形区域，方便调试
    private void OnDrawGizmosSelected()
    {
        if (drawGizmo == false) return; 

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Vector3 leftLimit = Quaternion.Euler(0, -attackAngle / 2f, 0) * transform.forward;
        Vector3 rightLimit = Quaternion.Euler(0, attackAngle / 2f, 0) * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, leftLimit * attackRadius);
        Gizmos.DrawRay(transform.position, rightLimit * attackRadius);
    }
}
