using UnityEngine;

namespace DuckTown3.SkillSystemV2
{
    //注意现在现学一个非常专业和安全的写法
    //关于数组的写法

    //技能栏的背后数据
    public class SkillBar
    {
        private SkillDataBaseSO[] barSlots;

        //问题：那个没有参数的构造器存在吗？
        //回答：因为指定了其他带参数构造器，无参默认构造器不存在
        public SkillBar(int size)
        { 
            barSlots = new SkillDataBaseSO[size];
        }

        public int GetBarSize() 
        {
            //Debug.Log("barSlots.Length: " + barSlots.Length);
            return barSlots.Length;
        }

        public void SetSlot(SkillDataBaseSO data, int index)
        {
            if (index >= 0 && index < barSlots.Length)
            {
                barSlots[index] = data;
            }
        }

        public SkillDataBaseSO GetSlot(int index)
        {
            if (index >= 0 && index < barSlots.Length)
            { 
                return barSlots[index];
            }
            return null;
        }

        //预留
        private void Save() { }
        private void Load() { }
    }
}