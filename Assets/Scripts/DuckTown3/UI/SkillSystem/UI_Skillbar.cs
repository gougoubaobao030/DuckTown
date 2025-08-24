using DuckTown3.SkillSystemV2;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using System.Net.NetworkInformation;
using static UnityEngine.Rendering.VolumeComponent;
using UnityEngine.InputSystem.iOS;

//现在暂时其他的什么也不管
//只负责把skillbar里的技能显示出来
//还有生成slots
public class UI_Skillbar : MonoBehaviour
{
    [SerializeField]private GameObject slotPrefab;
    [SerializeField]private Transform slotParent;

    [SerializeField]private Canvas canvas;

    //奇怪但是其实我不知道的错误，这里没有new出来
    private List<UI_SkillBarSlot> slotsList = new();
    //听说从老大注入，说不定待会儿崩了就是因为从老大注入（笑）
    private SkiillManager skillManager;
    private SkillBar skillBar;

    public void Init(SkillBar skillBar, SkiillManager skiillManager)
    {
        this.skillManager = skiillManager;
        this.skillBar = skillBar;

        for (int i = 0; i < skillBar.GetBarSize(); i++)
        {
            //**重要！！**
            //这段代码跑在prefab start()之前
            var slot = Instantiate(slotPrefab, slotParent);
            var script = slot.GetComponent<UI_SkillBarSlot>();
            script.Init(canvas, this, i);
            var data = skillBar.GetSlot(i);
            if (data != null)
            {
                var skill = skillManager.GetSkillAtIndex(i);
                script.SetSkill(skill, data.Icon);
            }

            int tmp = i;
            //Debug.Log((IntPtr)(&tmp));
            //这条注释只想说明每次地址都不一样；
            //闭包问题
            script.InjectCallback(() => skiillManager.OnUISkillSlotClicked(tmp));

            slotsList.Add(script);
        }
    }

    public void SwapSlot(int indexA, int indexB)
    {
        (slotsList[indexA], slotsList[indexB]) = (slotsList[indexB], slotsList[indexA]);

        // 重建顺序（或强行交换位置）
        for (int i = 0; i < slotsList.Count; i++)
        {
            slotsList[i].transform.SetSiblingIndex(i); // 这句能让 UI 顺序生效
            //slotsList[i].UpdateIndex(i); // 可选，用来同步 index
        }

    }

    public void RequestSwap(int indexA, int indexB)
    {
        skillManager.SwapSkills(indexA, indexB);
        RefreshSkillbarUI();
    }

    private void RefreshSkillbarUI()
    {
        for (int i = 0; i < slotsList.Count; i++)
        {
            Sprite icon;
            var data = skillBar.GetSlot(i);
            if (data != null)
            {
                icon = data.Icon;
            }
            else
            {
                icon = null;
            }
            var skill = skillManager.GetSkillAtIndex(i);
            slotsList[i].SetSkill(skill, icon);
        }
    }

    public bool IsOnGlobalCD()
    { 
        return skillManager.IsOnGlobalCD();
    }

    public float GetGlobalCooldownPercent()
    { 
        return skillManager.GetGlobalCooldownPercent();
    }

}
