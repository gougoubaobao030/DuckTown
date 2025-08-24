using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using DuckTown3.TownInput;

namespace DuckTown3.SkillSystemV2
{
    //skill dazongguan
    //transfer data to skillbar
    //init executor

    public class SkiillManager : MonoBehaviour
    {
        //it is called caster in specific skills
        [SerializeField]private Transform slashPointer;
        [SerializeField]private SkillExecutor executor;
        [SerializeField]private UI_Skillbar uI_Skillbar;
        public SkillDataBaseSO[] skillslots = new SkillDataBaseSO[10];
        private SkillBar skillBar;

        private void Start()
        {
            skillBar = new SkillBar(skillslots.Length);

            for (int i = 0; i < skillslots.Length; i++)
            { 
                skillBar.SetSlot(skillslots[i], i);
            }

            ISkillInputProvider input = new KeyBoardMouseInputProvider();

            executor.Init(skillBar, slashPointer, input);
            uI_Skillbar.Init(skillBar, this);
        }

        public void OnUISkillSlotClicked(int index)
        { 
            //Debug.Log("UI clicked skill index: " + index);
            executor.CastSkill(index);
        }

        public void SwapSkills(int indexA, int indexB)
        { 
            var temp = skillBar.GetSlot(indexA);
            skillBar.SetSlot(skillBar.GetSlot(indexB), indexA);
            skillBar.SetSlot(temp, indexB);

            executor.RebuildAllSkills();
        }

        public ISkill GetSkillAtIndex(int index)
        { 
            //这里要不要防呆检测是个问题
            return executor.GetSkillInstacne(index);
        }

        //GCD
        public bool IsOnGlobalCD()
        {
            return executor.isOnGlobalCD;
        }

        public float GetGlobalCooldownPercent()
        { 
            return executor.GetGlobalCooldownPercent();
        }

    }
}
