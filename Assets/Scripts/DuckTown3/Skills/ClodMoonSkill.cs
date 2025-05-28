using UnityEngine;

[CreateAssetMenu(fileName = "MoonBladeNormal", menuName = "DuckTown3/Skills/MoonBlade")]
public class ClodMoonSkill : SkillData3
{
    [SerializeField] private float maxFlyDistance = 40.0f;
    [SerializeField] private float flySpeed = 15.0f;
    [SerializeField] private Vector3 spawnOffset = new Vector3(0, 0, 0);
    [SerializeField] private float rotZ = 90.0f;
    //private Quaternion rot = Quaternion.Euler(0, 0, 90.0f);
    private Transform _slashPointer;
    public Transform SlashPointer => _slashPointer != null ? _slashPointer : _slashPointer = Duck3.instance.SlashPointer;

    public override void SkillBehavior()
    {

        if (effectPreFab != null && SlashPointer != null)
        { 
            var spwanPointer = SlashPointer.TransformPoint(spawnOffset);
            GameObject coldmoon = Instantiate(effectPreFab, spwanPointer, SlashPointer.rotation * Quaternion.Euler(0, 0, rotZ));

            var projectile = coldmoon.GetComponent<ColdMoonBladeProjectile3>();
            {
                if (projectile != null)
                {
                    projectile.Init(maxFlyDistance, flySpeed);
                }
            }
            
        }
    }
}
