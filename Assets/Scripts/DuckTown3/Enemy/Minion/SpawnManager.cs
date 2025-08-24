using DuckTown3.ObjectPool;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.GPUSort;

namespace DuckTown3
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject minionPrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private Transform parent;

        //duck injection
        [SerializeField] private Transform duck;

        private void OnEnable()
        {
            EnemyYellowGay3.OnMinionDied += EnemyYellowGay3_OnMinionDied;
        }

        private void EnemyYellowGay3_OnMinionDied(Vector3 vector, Quaternion quaternion, float arg3)
        {
            StartCoroutine(ReSpawn(vector, quaternion, arg3));
        }

        private void OnDisable()
        {
            EnemyYellowGay3.OnMinionDied -= EnemyYellowGay3_OnMinionDied;
        }


        private void Start()
        {
            foreach (var spawnPoint in spawnPoints)
            {
                var minion = ObjectPoolManager.Instance.Get(minionPrefab);
                var script = minion.GetComponent<EnemyYellowGay3>();
                script.SetPosAndRotInfo(spawnPoint.position, spawnPoint.rotation);
                script.SetPosAndRot();
            
                var ctrl = minion.GetComponent<MinionController>();
                ctrl.SetTarget(duck);
                ctrl.SetSpawnPoint(spawnPoint.position);
            }


        }

        private IEnumerator ReSpawn(Vector3 pos, Quaternion rot, float delay)
        { 
            yield return new WaitForSeconds(delay);
            var minion = ObjectPoolManager.Instance.Get(minionPrefab);
            //minion.transform.SetParent(parent);
            //minion.transform.position = pos;
            //minion.transform.rotation = rot;
            //var script = minion.GetComponent<EnemyYellowGay3>();
            //script.SetPosAndRotInfo(spawnPoint.position, spawnPoint.rotation);
            //script.SetPosAndRot();
        }
    }
}
