using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;

public class VFXPoolManager : MonoBehaviour
{
    //temp
    //will change to Addressables
    [Header("Effect Prefabs")]
    [SerializeField] private GameObject dashEffectPrefab;

    //以后会升级到dic管理
    private ObjectPool<GameObject> dashEffectPool;

    public void InitVFXPool()
    {
        dashEffectPool = new ObjectPool<GameObject>(
            //可以理解这是一个工厂，在初期化的时候做了很多设定
            createFunc: () =>
            {
                var go = Instantiate(dashEffectPrefab);
                var releaseScript = go.GetComponent<PoolRelease>() ?? go.AddComponent<PoolRelease>();
                //避免闭包风险，但我完全不懂啊
                releaseScript.InjectCallBack(() => dashEffectPool.Release(releaseScript.gameObject));
                return go;
            },
            actionOnGet: go => go.SetActive(true),
            actionOnRelease: go => go.SetActive(false),
            actionOnDestroy: go => Destroy(go),
            collectionCheck: false,
            defaultCapacity: 3,
            maxSize: 6
        );
    }

    //工业经典黄金封装
    //方便对象池的替换
    //逻辑的扩展
    //方便调试
    public GameObject GetDashEffect()
    {
        return dashEffectPool.Get();
    }
}
