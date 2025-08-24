using UnityEngine;

namespace DuckTown3
{
    public class MonoBehaviourHelper : MonoBehaviour
    {
        public static MonoBehaviourHelper Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            { 
                Destroy(gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
