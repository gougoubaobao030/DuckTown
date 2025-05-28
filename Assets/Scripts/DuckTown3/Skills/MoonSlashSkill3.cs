using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MoonSlashNormal", menuName = "DuckTown3/Skills/MoonSlash")]
public class MoonSlashSkill3 : SkillData3
{
    public Vector3 spawnPointerOffset = new Vector3(0, 0, 0);

    private Collider[] colliders = new Collider[10];

    public float attackRadius = 5.0f;
    public float attackAngle = 160.0f;

    //lazy load
    private Transform _slashPointer;
    public Transform slashPoiter => _slashPointer != null ? _slashPointer : (_slashPointer = Duck3.instance?.SlashPointer);

    public float testAngle = -15.0f;
    public bool drawGzimo = true;
    //按下攻击键后的动作
    public override void SkillBehavior()
    {

        if (effectPreFab != null && slashPoiter != null)
        {
            var spawnPonter = slashPoiter.position + spawnPointerOffset;
            GameObject moonSlashEffect = Instantiate(effectPreFab, spawnPonter, slashPoiter.rotation * Quaternion.Euler(0, testAngle, 0));
            ParticleSystem ps = moonSlashEffect.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                ps.Play();
            }
        }

        int hitCounts = Physics.OverlapSphereNonAlloc(slashPoiter.position, attackRadius, colliders, enemyLayer);
        for (int i = 0; i < hitCounts; i++)
        {
            Vector3 dir = (colliders[i].transform.position - slashPoiter.position).normalized;
            float angle = Vector3.Angle(slashPoiter.forward, dir);

            if (angle < attackAngle / 2)
            {
                IAttackable enemy = colliders[i].GetComponent<IAttackable>();
                if (enemy != null)
                {
                    enemy.TakeDamage();
                }
            }
        }
    }

    public void DrawSkillGizmo()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(slashPoiter.position, attackRadius);

        Vector3 leftLimit = Quaternion.Euler(0, -attackAngle / 2.0f, 0) * slashPoiter.forward;
        Vector3 rightLimit = Quaternion.Euler(0, attackAngle / 2.0f, 0) * slashPoiter.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(slashPoiter.position, leftLimit * attackRadius);
        Gizmos.DrawRay(slashPoiter.position, rightLimit * attackRadius);
    }

}
