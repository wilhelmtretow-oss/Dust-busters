using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;      // max HP
    private int currentHealth;
    public bool deathDrop = false;
    [SerializeField] private GameObject stinkCloudPrefab;

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
            // Lägg till progress
            CleaningManager manager = FindObjectOfType<CleaningManager>();
            if (manager != null)
                manager.AddCleanedObject();
            if (deathDrop && stinkCloudPrefab != null)
            {
                GameObject Stink_cloud = Instantiate(stinkCloudPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    // Döda fienden
    void Die()
    {
        // Här kan du lägga till partiklar eller animation om du vill
        Destroy(gameObject);
    }
}