using UnityEngine;

namespace DuckTown3.SkillSystemV1
{
    public class SlashTest3 : MonoBehaviour
    {
        public GameObject ColdMoon;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                GameObject coldMoon = Instantiate(ColdMoon, transform.position, Quaternion.identity);
            }
        }
    }
}