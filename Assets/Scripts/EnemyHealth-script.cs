using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;      // max HP
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Ta skada från projektiler
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        // Om fienden dör
        if (currentHealth <= 0)
        {
            Die();
            FindObjectOfType<CleaningManager>().AddCleanedObject();
        }
    }

    // Döda fienden
    void Die()
    {
        // Här kan du lägga till partiklar eller animation om du vill
        Destroy(gameObject);
    }
}