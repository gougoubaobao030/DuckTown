using UnityEngine;

namespace DuckTown3.ObjectPool
{
    public class PoolReleaseNormal : MonoBehaviour
    {
        private System.Action OnPoolReleased;

        public void InjectCallback(System.Action callback)
        { 
            OnPoolReleased = callback;
        }

        private void OnDisable()
        {
            OnPoolReleased?.Invoke();
        }
    }
}