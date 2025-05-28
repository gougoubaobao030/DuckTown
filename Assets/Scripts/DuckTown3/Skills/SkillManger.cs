using NUnit.Framework;
using UnityEngine;

public class SkillManger : MonoBehaviour
{
    public SkillData3[] skills;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skills[0].SkillBehavior();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skills[1].SkillBehavior();
        }
    }
}
