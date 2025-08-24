using UnityEngine;
using System.Collections.Generic;

namespace DuckTown3.ObjectPool
{
    //听智障说大型项目都是这样单例管理的
    //不知道真假，调用了再说
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get; private set; }

        private Dictionary<GameObject, Queue<GameObject>> poolDic = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //can use asyncpreload
        //but not now
        public void PreLoad(GameObject prefab, int count)
        {
            if (!poolDic.ContainsKey(prefab))
            { 
                poolDic[prefab] = new Queue<GameObject>();
            }

            for (int i = 0; i < count; i++)
            {
                var obj = Create(prefab);
                poolDic[prefab].Enqueue(obj);
            }
        }

        private GameObject Create(GameObject prefab)
        { 
            var obj = Instantiate(prefab, transform);
            obj.SetActive(false);

            var release = obj.GetComponent<PoolReleaseNormal>();
            if (release == null)
            {
                release = obj.AddComponent<PoolReleaseNormal>();
            }
            release.InjectCallback(() => ReturnToPool(prefab, obj));

            return obj;
        }

        public GameObject Get(GameObject prefab)
        {
            if (!poolDic.TryGetValue(prefab, out var queue))
            {
                queue = new Queue<GameObject>();
                poolDic[prefab] = queue;
            }

            GameObject obj;
            if (queue.Count > 0)
            {
                obj = queue.Dequeue();
            }
            else
            {
                obj = Create(prefab);
            }

            obj.SetActive(true);
            return obj;
        }

        public void ReturnToPool(GameObject prefab, GameObject instance)
        {
            instance.SetActive(false);
            poolDic[prefab].Enqueue(instance);
        }

    }
}