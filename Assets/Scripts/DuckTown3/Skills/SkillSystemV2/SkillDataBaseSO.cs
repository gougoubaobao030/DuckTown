using DuckTown3.TownInput;
using UnityEngine;

namespace DuckTown3.SkillSystemV2
{
    public abstract class SkillDataBaseSO : ScriptableObject
    {
        //自己的函数类里面隐藏了一个Skilldatabase data.
        //怕忘了，提醒一下
        public Sprite Icon;

        [Header("CD")]
        public float CooldownTime;
        public abstract ISkill CreateSkill(Transform casterPointer, ISkillInputProvider input);
    }
}