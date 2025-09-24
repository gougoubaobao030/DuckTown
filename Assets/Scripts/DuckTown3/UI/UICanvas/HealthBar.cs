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
            //Debug.Log("setmaxhealth used");
        }

        public void SetHealth(float health)
        {
            slider.DOValue(health, 0.3f);
            //Debug.Log("sethealth used");
        }
    }
}