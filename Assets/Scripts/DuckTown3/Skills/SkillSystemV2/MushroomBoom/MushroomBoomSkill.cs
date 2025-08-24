using DuckTown3.ObjectPool;
using DuckTown3.TownInput;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace DuckTown3.SkillSystemV2
{
    public class MushroomBoomSkill : SkillBase<MushroomBoomSO>
    {
        //private MushroomBoomSO data;
        //private Transform caster;
        //private ISkillInputProvider input;

        //private float cooldownTimer = 0.0f;

        //public event Action OnSkillExecuted;

        //public bool isOnCoolDown
        //{
            //get { return cooldownTimer > 0.0f; }
        //}

        public MushroomBoomSkill(MushroomBoomSO data, Transform caster, ISkillInputProvider input):
            base(data, caster, input) 
        { 
            
            //this.caster = caster;
            //this.input = input;
        }

        public override void BeHavior()
        {
            if (isOnCoolDown)
            {
                Debug.Log("is Cool Down");
                return;
            }

            Vector3 shootPointer = caster.TransformPoint(data.SpawnOffset);

            var mushroom = ObjectPoolManager.Instance.Get(data.MushroomPrefab);
            if (mushroom != null)
            {
                mushroom.transform.position = shootPointer;
                mushroom.transform.rotation = caster.rotation;

                Rigidbody rb = mushroom.GetComponent<Rigidbody>();

                Vector3 throwDir = caster.forward * 1.0f + caster.up * data.YForce;
                rb.linearVelocity = throwDir.normalized * data.MushroomSpeed;
            }

            //OnSkillExecuted?.Invoke();
            //begin cooldown
            //cooldownTimer = data.CooldownTime;
            TriggerCooldown();
        }

        //public override void Tick()
        //{
            //if (cooldownTimer > 0.0f)
            //{
                //cooldownTimer -= Time.deltaTime;
            //}
        //}

        //public float GetCoolDownPrecent()
        //{
            //return isOnCoolDown ? cooldownTimer / data.CooldownTime : 0f;
        //}
    }
}
