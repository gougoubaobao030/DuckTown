using DuckTown3.TownInput;
using UnityEngine;


namespace DuckTown3.SkillSystemV2
{
    [CreateAssetMenu(fileName = "MushroomBoom", menuName = "DuckTown3/Skills/MushroomBoom")]
    public class MushroomBoomSO : SkillDataBaseSO
    {
        [Header("Basic Info")]
        public string SkillName;
        [TextArea]
        public string Description;
        //public Sprite SkillIcon;
        public GameObject MushroomPrefab;

        [Header("CD")]
        //public float CooldownTime = 2.0f;

        [Header("ThrowData")]
        public float YForce = 1.0f;
        public float MushroomSpeed = 15.0f;

        [Header("BoomData")]
        public float ExplosionDelay = 3.6f;
        //public float Damage = 30.0f;
        public float AttackRadius = 8.0f;
        public GameObject ExplosionVFXPrefab;
        public LayerMask AttackableLayer;

        [Header("ShootPointer")]
        public Vector3 SpawnOffset = Vector3.zero;

        public override ISkill CreateSkill(Transform casterPointer, ISkillInputProvider input)
        {
            return new MushroomBoomSkill(this, casterPointer, input);
        }

    }
}
