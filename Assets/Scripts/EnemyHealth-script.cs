using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    public bool deathDrop = false;
    [SerializeField] private GameObject stinkCloudPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount) // when taking damage save as int
    {
        currentHealth -= amount; // remove int "amount" from hp
        if (currentHealth <= 0)
        {
            Die();
            if (CleaningManager.Instance != null) // if removed
                CleaningManager.Instance.AddCleanedObject(); // become a "cleaned object" progressing the mission
            if (deathDrop && stinkCloudPrefab != null) // if it has "deathDrop" and stinkPrefab is present
                Instantiate(stinkCloudPrefab, transform.position, Quaternion.identity); // place stinkCloudPrefab on top of itself
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}