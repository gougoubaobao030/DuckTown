using UnityEngine;

namespace DuckTown3.UI
{
    public class WorldUICanvas : MonoBehaviour
    {
        public static WorldUICanvas Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            { 
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}
