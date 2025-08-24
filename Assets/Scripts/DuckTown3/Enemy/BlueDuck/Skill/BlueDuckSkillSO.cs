using UnityEngine;

namespace DuckTown3.Enemy
{
    //[CreateAssetMenu(fileName = "BlueDuckSO", menuName = "DuckTown3/EnemySkill/BlueDuck")]
    public abstract class BlueDuckSkillSO : ScriptableObject
    {
        public string SkillName;
        public string SkillAnimName;
        public GameObject EffectPrefab;

        [Header("Skill Data")]
        public bool canCombo;
        public BlueDuckSkillSO nextSkill = null;
        public Vector3 SkillOffset = Vector3.zero;
        public Vector3 SkillRotateOffset = Vector3.zero;

        [Header("Attack Data")]
        public float damage = 99.0f;
        public LayerMask targetLayer;

        public abstract void SkillBeHavior(EnemyController blueDuck);
    }

}
