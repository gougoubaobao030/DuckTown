using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DuckTown3.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void SetMaxHealth(float maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        public void SetHealth(float health)
        {
            slider.DOValue(health, 0.3f);
        }
    }
}