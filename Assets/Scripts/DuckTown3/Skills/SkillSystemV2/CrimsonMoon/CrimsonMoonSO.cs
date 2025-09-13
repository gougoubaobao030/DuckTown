using DuckTown3.SkillSystemV2;
using DuckTown3.TownInput;
using UnityEngine;

namespace DuckTown3.SkillSystemV2
{
    [CreateAssetMenu(fileName = "CrimsonMoonSO", menuName = "DuckTown3/Skills/CrimsonMoon")]
    public class CrimsonMoonSO : SkillDataBaseSO
    {
        [Header("Basic Info")]
        public string SkillName;
        [TextArea]
        public string Description;
        //public Sprite SkillIcon;
        public GameObject LotusShadowPrefab;
        public GameObject CrimsonMoonPrefab;

        [Header("CD")]
        //public float CooldownTime = 1.0f;

        [Header("CastData")]
        public float DamageDelayTime = 1.0f;
        public float Radius = 3.0f;
        //public float Damage = 50.0f;
        public float ShadowMaxDistance = 100.0f;
        public LayerMask DamageLayer;
        public LayerMask ShadowLayer;

        public override ISkill CreateSkill(Transform casterPointer, ISkillInputProvider input)
        {
            return new CrimsonMoonSkill(this, casterPointer, input);
        }
    }
}