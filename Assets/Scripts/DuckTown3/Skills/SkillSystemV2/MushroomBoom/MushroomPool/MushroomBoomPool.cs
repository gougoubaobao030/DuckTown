using UnityEngine;
using System.Collections.Generic;

namespace DuckTown3.SkillSystemV2
{
    //这是一个试验用的对象池
    //继承使用mono方便测试
    public class MushroomBoomPool : MonoBehaviour
    {
        [SerializeField] private int initializeSize = 3;

        //有特别需要吗
        //private GameObject mushroomBombPrefab;
        private Queue<GameObject> pool = new Queue<GameObject>();
        private bool isInitialized { get; set; } = false;

        public void InitPool(GameObject prefab)
        {
            if (isInitialized) return;

            for (int i = 0; i < initializeSize; i++)
            {
                var mushroom = CreateMushroom(prefab);
                pool.Enqueue(mushroom);
            }

            isInitialized = true;
        }

        private GameObject CreateMushroom(GameObject prefab)
        {
            var mushroom = Instantiate(prefab, transform);
            mushroom.SetActive(false);

            //内部添加蘑菇脚本
            //以后的大池子还是需要外部添加
            //获得脚本
            var release = mushroom.GetComponent<MushroomPoolReleased>();
            if (release == null)
            { 
                release = mushroom.AddComponent<MushroomPoolReleased>();
            }

            //inject delegate callback
            //multithread safe
            var captured = mushroom;
            release.Inject(() => ReturnMushroom(captured));

            return mushroom;
        }

        public GameObject Get(GameObject prefab)
        {
            if (prefab == null)
            {
                Debug.LogError("mushroomBombPrefab is null, call init first or check data");
            }

            GameObject mushroom = null;

            if (pool.Count > 0)
            {
                mushroom = pool.Dequeue();

            }
            else
            {
                mushroom = CreateMushroom(prefab);
            }

            mushroom.SetActive(true);

            //这里可以加上初始化重置逻辑


            return mushroom;
        }

        public void ReturnMushroom(GameObject mushroom)
        {
            if (mushroom == null) return; 

            mushroom.SetActive(false);
            pool.Enqueue(mushroom);
        }
    }
}