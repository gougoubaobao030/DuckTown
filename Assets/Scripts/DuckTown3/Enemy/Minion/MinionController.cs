using DuckTown3.ObjectPool;
using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace DuckTown3
{
    public class MinionController : MonoBehaviour
    {
        
        public enum MinionState { Idle, Chase, Attack, Return, Dead };

        [SerializeField] private MinionSO data;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        //[SerializeField] private GameObject minionPrefab;

        private Transform duck;
        private Vector3 spawnPoint;
        private MinionState currentState = MinionState.Idle;
        

        private bool isDead = false;

        private void Awake()
        {
            //spawnPoint = transform.position;
            //Debug.LogWarning("Minon Awake now");
        }

        private void OnEnable()
        {
            //spawnPoint = transform.position;
            //这种重置就让人觉得很妙
            //幂等性
            currentState = MinionState.Idle;
            //Debug.LogWarning("Minion enable now");
        }

        private void OnDisable()
        {
            //好的，不知道的时候不要瞎搞
            //agent.ResetPath();
            animator.SetBool("isAttack", false);
            //Debug.LogWarning("Minion disable now");
        }

        private void Start()
        {
            animator.Play("MinionIdle");
            //Debug.LogWarning("Minion start now");
        }

        float dist;
        private void Update()
        {
            dist = Vector3.Distance(transform.position, duck.position);

            switch (currentState)
            { 
                case MinionState.Idle:
                    HandleIdle();
                    break;

                case MinionState.Chase:
                    HandleChase();
                    break;

                case MinionState.Attack:
                    HandleAttack();
                    break;

                case MinionState.Return:
                    HandleReturn();
                    break;

                case MinionState.Dead:
                    HandleDie();
                    break;
            }

            //Debug.Log("Minion's currentState: " + currentState);
        }

        private void HandleIdle()
        {
            if (dist < data.chaseRange)
            { 
                currentState = MinionState.Chase;
            }
        }

        private void HandleChase()
        {
            if (dist > data.giveUpRange || DistFromSpawn() > data.DistFromSpawn)
            {
                currentState = MinionState.Return;
                return;
            }

            agent.SetDestination(duck.position);

            if (dist < data.attackRange)
            {
                currentState = MinionState.Attack;
            }
        }

        private void HandleAttack()
        {
            agent.ResetPath();
            //transform.LookAt(duck);

            //transform.LookAt(lookDir);


            animator.SetBool("isAttack", true);

            if (dist > data.attackRange)
            {
                currentState = MinionState.Chase;
                animator.SetBool("isAttack", false);
            }

            RotateToTarget();
        }

        private void HandleReturn()
        {
            agent.SetDestination(spawnPoint);
            float planeDist = DistFromSpawn();
            //Debug.Log("planeDist: " + planeDist);

            //至于为什么1.5f, 因为agent设置的停顿距离
            if (planeDist < 1.5f)
            {
                //Debug.Log("is Idle now");
                currentState = MinionState.Idle;
            }
        }
        private void RotateToTarget()
        {
            Vector3 lookDir = duck.position - transform.position;
            lookDir.y = 0;
            //Debug.Log(lookDir);
            Quaternion rotate = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, 8.0f * Time.deltaTime);
        }

        private float DistFromSpawn()
        {
            Vector2 tp2 = new Vector2(transform.position.x, transform.position.z);
            Vector2 sp2 = new Vector2(spawnPoint.x, spawnPoint.z);

            float planeDist = Vector2.Distance(tp2, sp2);
            return planeDist;
        }

        private void HandleDie()
        {
            agent.ResetPath();
        }

        private void ChangeState()
        {
            //之后再说
            //有个编译好的关于grahf的bug
        }

        private void MinionDie()
        { 
            
        }

        //public IEnumerator Respawn()
        //{
        //    yield return new WaitForSeconds(data.respawnDelay);
        //    var minion = ObjectPoolManager.Instance.Get(minionPrefab);
        //    minion.transform.position = spawnPoint;
        //}

        public void SetTarget(Transform target)
        { 
            duck = target;
        }

        public void SetSpawnPoint(Vector3 pos)
        { 
            spawnPoint = pos;
        }
    }
}
