using DuckTown3.TownInput;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

namespace DuckTown3.SkillSystemV2
{
    //Skill System CEO
    //Executor is used for 
    //1. change sodata from skillbar to Iskill instance
    //2. update cast() and tick()
    public class SkillExecutor : MonoBehaviour
    {
        private SkillBar skillBar;
        private ISkill[] skills;
        private Transform caster;
        private ISkillInputProvider input;

        //GCD
        [SerializeField]private float globalCoolDown = 0.5f;
        private float globalCoolDownTimer = 0.0f;

        public bool isOnGlobalCD => globalCoolDownTimer > 0.0f;

        public void Init(SkillBar skillbar, Transform caster, ISkillInputProvider input)
        { 
            this.skillBar = skillbar;
            this.caster = caster;
            this.input = input;

            skills = new ISkill[skillbar.GetBarSize()];

            InstantiaSkills();
        }

        private void InstantiaSkills()
        {
            for (int i = 0; i < skillBar.GetBarSize(); i++)
            {
                var data = skillBar.GetSlot(i);
                if (data != null)
                {
                    skills[i] = data.CreateSkill(caster, input);
                    skills[i].OnSkillExecuted += () =>
                    {
                        globalCoolDownTimer = globalCoolDown;
                    };
                }
            }
        }

        private void Update()
        {
            if (globalCoolDownTimer > 0.0f)
            { 
                globalCoolDownTimer -= Time.deltaTime;
            }

            //should changed by public interface ISkillInputProvider
            for (int i = 0; i < skills.Length; i++)
            {
                //if (Input.GetKeyDown(KeyCode.Alpha1 + i)) CastSkill(i);
                if(input.IsSkillKeyPressed(i)) CastSkill(i);

                skills[i]?.Tick();
            }
        }

        public void CastSkill(int index)
        {
            if (isOnGlobalCD)
            {
                Debug.Log("On Global CD");
                return;
            }

            if (index >= 0 && index < skills.Length)
            {
                skills[index]?.BeHavior();
            }

        }

        public void RebuildAllSkills()
        {
            for (int i = 0; i < skillBar.GetBarSize(); i++)
            {
                if (skills[i] != null)
                {
                    skills[i].OnSkillExecuted -= OnSkillExecutedHandler;
                }

                var data = skillBar.GetSlot(i);
                //skills[i] = data != null ? data.CreateSkill(caster, input) : null;
                if (data != null)
                {
                    skills[i] = data.CreateSkill(caster, input);
                    skills[i].OnSkillExecuted += OnSkillExecutedHandler;
                }
                else
                { 
                    skills[i] = null;
                }
            }
        }

        private void OnSkillExecutedHandler()
        {
            globalCoolDownTimer = globalCoolDown;
        }

        public ISkill GetSkillInstacne(int index)
        {
            if (index >= 0 && index < skills.Length)
            { 
                return skills[index];
            }

            return null;
        }

        public float GetGlobalCooldownPercent()
        {
            return isOnGlobalCD ? globalCoolDownTimer / globalCoolDown : 0.0f;
        }

    }
}