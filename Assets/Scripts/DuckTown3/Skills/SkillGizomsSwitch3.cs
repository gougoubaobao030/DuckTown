using Unity.VisualScripting;
using UnityEngine;

public class SkillGizomsSwitch3 : MonoBehaviour
{
    public MoonSlashSkill3 moonSlash;

    private void OnDrawGizmos()
    {
        if(!moonSlash.drawGzimo) return;

        moonSlash.DrawSkillGizmo();
    }
}
