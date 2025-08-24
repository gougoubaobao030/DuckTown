using DuckTown3.TownInput;
using UnityEngine;

namespace DuckTown3.SkillSystemV2
{
    [CreateAssetMenu(fileName = "ColdBladeMoon", menuName = "DuckTown3/Skills/ColdMoon")]
    public class ColdBladeMoonSO : SkillDataBaseSO
    {
        //icon
        //cd
        [Header("Basic Info")]
        public string SkillName;
        [TextArea]
        public string Description;

        [Header("Prefab")]
        public GameObject ColdMoonPrefab;

        [Header("Basic Data")]
        public float maxFlyDistance = 40.0f;
        public float flySpeed = 15.0f;
        public float rotZ = 90.0f;
        public LayerMask enemyLayer;

        [Header("Spawn Offset")]
        public Vector3 spawnOffset = Vector3.zero;

        public override ISkill CreateSkill(Transform casterPointer, ISkillInputProvider input)
        {
            return new ColdBladeMoonSkill(this, casterPointer, input);
        }
    }
}
