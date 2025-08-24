using System;
using UnityEngine;

namespace DuckTown3.SkillSystemV2
{
    public interface ISkill
    {
        void BeHavior();
        void Tick();
        bool isOnCoolDown { get; }
        float GetCoolDownPrecent();

        event Action OnSkillExecuted;
    }
}
