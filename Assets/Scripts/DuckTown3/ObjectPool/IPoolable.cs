using UnityEngine;

namespace DuckTown3.ObjectPool
{
    //先写上以防万一
    public interface IPoolable
    {
        //在get或者return的时候使用，重置一些数据，虽然我还不知道什么数据
        void OnGetFromPool();
        void OnReturnToPool();
    }
}