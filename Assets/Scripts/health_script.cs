using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;

    public int CurrentHealth { get; private set; }
    public bool isDead { get; private set; }

    void Start()
    {
        CurrentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        CurrentHealth -= amount;

        if (CurrentHealth < 0)
            CurrentHealth = 0;

        Debug.Log("Player health: " + CurrentHealth);

        if (CurrentHealth == 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player died!");

        // Stäng av rörelse om du vill
        // GetComponent<PlayerMovement>().enabled = false;

        // Här kan du aktivera Game Over UI
        // FindObjectOfType<GameManager>().GameOver();
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        CurrentHealth += amount;

        if (CurrentHealth > maxHealth)
            CurrentHealth = maxHealth;

        Debug.Log("Player healed: " + CurrentHealth);
    }
}