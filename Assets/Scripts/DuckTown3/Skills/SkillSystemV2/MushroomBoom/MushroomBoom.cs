using System.Collections;
using UnityEngine;
using DuckTown3.ObjectPool;

namespace DuckTown3.SkillSystemV2
{
    public class MushroomBoom : MonoBehaviour
    {
        [SerializeField] private GameObject explosionEffect;
        [SerializeField] private float ExplosionTime = 3.6f;

        [SerializeField] private float explosionRadius = 8.0f;
        [SerializeField] private LayerMask boomLayer;

        private Coroutine countdownCoroutine;

        //防御性exploded, 但在这里我还没感觉到实际意义。
        private bool isExploded = false;

        private void OnEnable()
        {
            isExploded = false;
            //开始倒计时
            countdownCoroutine = StartCoroutine(DelayExplosion());
        }

        private void Start()
        {
            //coroutine moved to onenable
        }

        IEnumerator DelayExplosion()
        {
            yield return new WaitForSeconds(ExplosionTime);
            Explode();
        }

        private void Explode()
        {
            if (isExploded) return;
            isExploded = true;

            //Debug.Log("BOOM");
            if (explosionEffect != null)
            {
                //var effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                var effect = ObjectPoolManager.Instance.Get(explosionEffect);
                effect.transform.position = transform.position;
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning("explosion effect is null");
            }
#endif

            //预留音效

            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, boomLayer);
            foreach (var hit in hits)
            {
                IAttackable enemy = hit.gameObject.GetComponent<IAttackable>();
                if (enemy != null)
                {
                    enemy.TakeDamage();
                }
            }

            gameObject.SetActive(false);
        }

        //遇到火焰或者下雨提前终止
        public void TriggerExplosionNow()
        {
            //stop coroutine
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
            }
            Explode();
        }

        private void OnDisable()
        {
            //考虑一下停掉协程
            //防御性编程
        }
    }
}