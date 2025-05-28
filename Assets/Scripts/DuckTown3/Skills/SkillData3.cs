using System.Diagnostics.Contracts;
using UnityEngine;

public abstract class SkillData3: ScriptableObject
{
    public string SkillName;
    public string SkillDescription;
    
    public GameObject effectPreFab;
    public LayerMask enemyLayer;

    //这个不能有
    //private Transform effectSpawnPonter = Duck3.instance.transform.Find("SlashPointer");
    
    //用来实装攻击碰撞
    public abstract void SkillBehavior();
}
