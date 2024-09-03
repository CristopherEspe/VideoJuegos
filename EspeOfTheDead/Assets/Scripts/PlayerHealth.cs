using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            // Lógica para manejar la muerte del jugador
            //Debug.Log("Player is dead.");
        }
    }
    
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {

            if (currentHealth <= 30)
            {
                healthBar.color = Color.red;
            }
            else
            {
                healthBar.color = Color.green;
            }

            healthBar.fillAmount = (float)currentHealth / maxHealth;
            //Debug.Log("Health bar updated: " + healthBar.fillAmount);

        }
    }
}
