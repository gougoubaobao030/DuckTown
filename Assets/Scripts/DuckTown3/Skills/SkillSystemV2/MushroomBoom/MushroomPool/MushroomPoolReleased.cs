using UnityEngine;

namespace DuckTown3.SkillSystemV2
{
    public class MushroomPoolReleased : MonoBehaviour
    {
        private System.Action OnPoolReleased;

        public void Inject(System.Action callback)
        {
            OnPoolReleased = callback;
        }

        private void OnDisable()
        {
            OnPoolReleased?.Invoke();
        }
    }
}