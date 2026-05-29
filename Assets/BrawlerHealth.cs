using UnityEngine;
using UnityEngine.UI; // Crucial: This allows us to control UI Sliders

public class BrawlerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI Link")]
    public Slider healthSlider; // We will drag the UI slider here in Unity

    void Start()
    {
        currentHealth = maxHealth;

        // Initialize the health bar values
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took damage! Health: " + currentHealth);

        // Update the health bar visual
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " was DEFEATED!");

        // Figure out who won based on who just died
        string winnerName = (gameObject.name == "Player1") ? "BLUE" : "RED";

        // Find our GameManager and tell it to show the screen
        FindAnyObjectByType<GameManager>().TriggerGameOver(winnerName);

        gameObject.SetActive(false);
    }
}