using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;

    public GameObject minimapContainer;

    public int CurrentHealth { get; private set; }
    public bool isDead { get; private set; }

    void Start()
    {
        CurrentHealth = maxHealth;
        isDead = false;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }

        Debug.Log("Player health: " + CurrentHealth);
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Player died!");

        // Stop the player movement if the component exists
        var movement = GetComponent("PlayerMovement");
        if (movement != null)
        {
            var movementBehaviour = movement as Behaviour;
            if (movementBehaviour != null)
            {
                movementBehaviour.enabled = false;
            }
        }

        // Show Game Over
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
            minimapContainer.SetActive(false);

        // Stop the game
        Time.timeScale = 0f;
    }
}