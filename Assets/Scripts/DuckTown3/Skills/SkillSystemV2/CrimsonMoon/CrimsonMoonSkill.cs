using System.Collections;
using UnityEngine;
using DuckTown3.ObjectPool;
using DuckTown3.TownInput;
using System;
using Unity.XR.OpenVR;

namespace DuckTown3.SkillSystemV2
{
    public class CrimsonMoonSkill : SkillBase<CrimsonMoonSO>
    {
        //private CrimsonMoonSO data;
        //private Transform caster;
        //private ISkillInputProvider input;

        private GameObject shadowInstance;
        private Collider[] colliders = new Collider[10];
        private Vector3? groundHitPointer;
        private bool isSkillSelecting = false;

        //cooldown
        //private float cooldownTimer = 0.0f;

        //public event Action OnSkillExecuted;

        //public bool isOnCoolDown
        //{ 
            //get { return cooldownTimer > 0.0f;  }
        //}

        public CrimsonMoonSkill(CrimsonMoonSO data, Transform caster, ISkillInputProvider input)
            : base(data, caster, input)
        { 
            //this.data = data;
            //this.caster = caster;
            //this.input = input;
        }

        public override void BeHavior()
        {
            if(isOnCoolDown) return;

            if(isSkillSelecting) return;
            shadowInstance = ObjectPoolManager.Instance.Get(data.LotusShadowPrefab);
            isSkillSelecting = true;
        }

        public override void Tick()
        {
            //if (cooldownTimer > 0.0f)
            //{
                //cooldownTimer -= Time.deltaTime;
                //Debug.Log("is on debug");
            //}
            base.Tick();

            if (!isSkillSelecting) return;

            UpdateShadowPosition();

            if (input.IsConfirmKeyPressed())
            {
                var bloodMoon = ObjectPoolManager.Instance.Get(data.CrimsonMoonPrefab);
                bloodMoon.transform.position = groundHitPointer.Value;
                bloodMoon.transform.rotation = Quaternion.identity;

                MonoBehaviourHelper.Instance.StartCoroutine(DelayBoold(groundHitPointer.Value));


                ExitShadowMode();

                //OnSkillExecuted?.Invoke();
                //cooldownTimer = data.CooldownTime;
                TriggerCooldown();
            }
            else if (input.IsCancelKeyPressed())
            { 
                ExitShadowMode();
            }

        }

        private void UpdateShadowPosition()
        { 
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out RaycastHit hit, data.ShadowMaxDistance, data.ShadowLayer))
            //{ 
                //groundHitPointer = hit.point;
            //}

            if (input.TryGetGroundPosition(data.ShadowMaxDistance, data.ShadowLayer, out Vector3 hit))
            {
                groundHitPointer = hit;
                shadowInstance.transform.position = hit;
            }
        }

        private IEnumerator DelayBoold(Vector3 pos)
        {
            yield return new WaitForSeconds(data.DamageDelayTime);
            //Collider[] colliders = new Collider[10];
            int hitCount = Physics.OverlapSphereNonAlloc(pos, data.Radius, colliders, data.DamageLayer);
            for (int i = 0; i < hitCount; i++)
            {
                IAttackable attackable = colliders[i].GetComponent<IAttackable>();
                if (attackable != null)
                { 
                    attackable.TakeDamage();
                }
            }
        }

        private void ExitShadowMode()
        { 
            shadowInstance.SetActive(false);
            shadowInstance = null;
            groundHitPointer = null;
            isSkillSelecting = false;
        }

        //public float GetCoolDownPrecent()
        //{
            //return isOnCoolDown ? cooldownTimer / data.CooldownTime : 0f;
        //}
    }
}
