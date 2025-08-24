using UnityEngine;
using DuckTown3.UI;
using UnityEngine.InputSystem.Utilities;

namespace DuckTown3.Duck
{
    public class DuckHealth : MonoBehaviour
    {
        //temp should from so
        [SerializeField] private float maxHealth = 999.0f;
        [SerializeField] private HealthBar healthBar;

        private float currentHealth;

        private void Start()
        {
            healthBar.SetMaxHealth(maxHealth);
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damageAmount)
        { 
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);
            healthBar.SetHealth(currentHealth);
        }
    }
}
