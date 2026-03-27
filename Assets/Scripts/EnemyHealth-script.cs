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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
            if (CleaningManager.Instance != null)
                CleaningManager.Instance.AddCleanedObject();
            if (deathDrop && stinkCloudPrefab != null)
                Instantiate(stinkCloudPrefab, transform.position, Quaternion.identity);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}