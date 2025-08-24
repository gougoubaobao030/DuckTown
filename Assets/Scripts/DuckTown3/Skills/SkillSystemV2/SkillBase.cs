using System;
using UnityEngine;
using DuckTown3.TownInput;

namespace DuckTown3.SkillSystemV2
{
    public abstract class SkillBase<T> : ISkill where T : SkillDataBaseSO
    {
        protected T data;
        protected Transform caster;
        protected ISkillInputProvider input;

        //CD
        protected float cooldownTimer = 0.0f;
        public bool isOnCoolDown
        { 
            get { return cooldownTimer > 0.0f; }
        }

        protected SkillBase(T data, Transform caster, ISkillInputProvider input)
        { 
            this.data = data;
            this.caster = caster;
            this.input = input;
        }

        public event Action OnSkillExecuted;

        public abstract void BeHavior();

        public virtual void Tick()
        {
            if (cooldownTimer > 0.0f)
            {
                cooldownTimer -= Time.deltaTime;
            }
        }

        public float GetCoolDownPrecent()
        {
            return isOnCoolDown ? cooldownTimer / data.CooldownTime : 0.0f;
        }

        protected void TriggerCooldown()
        { 
            //这个写在一起好不好我不知道，反正先写在一起吧。
            cooldownTimer = data.CooldownTime;
            OnSkillExecuted?.Invoke();
        }
    }
}
