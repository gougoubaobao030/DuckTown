using Unity.VisualScripting;
using UnityEngine;

namespace DuckTown3.SkillSystemV1
{
    public class SkillGizomsSwitch3 : MonoBehaviour
    {
        public MoonSlashSkill3 moonSlash;

        private void OnDrawGizmos()
        {
            if (!moonSlash.drawGzimo) return;

            moonSlash.DrawSkillGizmo();
        }
    }
}
