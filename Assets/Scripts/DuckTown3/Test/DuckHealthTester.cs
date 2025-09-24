using UnityEngine;
using DuckTown3.UI;

public class DuckHealthTester : MonoBehaviour
{
    [SerializeField]private float maxHealth = 999f;
    [SerializeField] private float damageAmount = 99.0f;

    private float currentHealth;

    public HealthBar healthBar;

    private void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L))
        //{ 
            //TakeDamage(damageAmount);
        //}
    }

    //public void TakeDamage(float amount)
    //{ 
        //currentHealth -= amount;
        //currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        //healthBar.SetHealth(currentHealth);
    //}
}
