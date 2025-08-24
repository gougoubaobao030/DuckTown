using DuckTown3.TownInput;
using UnityEngine;

namespace DuckTown3.SkillSystemV2
{
    [CreateAssetMenu(fileName = "SlashMoonSO", menuName = "DuckTown3/Skills/SlashMoon")]
    public class SlashMoonSO : SkillDataBaseSO
    {
        [Header("Basic Info")]
        public string SkillName = "SlashMoon";
        [TextArea]
        public string Description; 

        [Header("Prefab")]
        public GameObject SlashPrefab;

        [Header("Basic Data")]
        public float attackRadius = 5.0f;
        public float attackAngle = 160.0f;
        public float testAngle = -15.0f;
        public LayerMask enemyLayer;


        [Header("Slash Pointer Offset")]
        public Vector3 slashPointerOffset = Vector3.zero;

        public override ISkill CreateSkill(Transform casterPointer, ISkillInputProvider input)
        {
            return new SlashMoonSkill(this, casterPointer, input);
        }
    }
}
