using System;
using UnityEngine;

public class Colony_creator : MonoBehaviour
{
    public GameObject Colony;

    float spawnTimer;
    float timer;
    string difficulty = ContractData.SelectedDifficulty;
    bool colonyStopSpawn = false;

    void Start()
    {
        // KOLLA OM FIENDER ÄR AVSTÄNGDA I INSTÄLLNINGARNA
        // Om "enemies" är 0, så stänger vi av spawn direkt
        int enemiesEnabled = PlayerPrefs.GetInt("enemies", 1); // 1 är standard (på)

        if (enemiesEnabled == 0)
        {
            colonyStopSpawn = true;
            Debug.Log("Colonies disabled due to settings.");
        }

        spawnTimer = UnityEngine.Random.Range(25, 45);
    }

    void Update()
    {
        // Om de är avstängda behöver vi inte ens köra timern
        if (colonyStopSpawn) return;

        timer += Time.deltaTime;

        if (timer >= spawnTimer)
        {
            SpawnColony();
            timer = 0;
            spawnTimer = UnityEngine.Random.Range(25, 45);
        }
    }

    void SpawnColony()
    {
        // Dubbelkoll så inget spawnar om colonyStopSpawn är true
        if (!colonyStopSpawn)
        {
            if (difficulty == "Easy")
            {
                Instantiate(Colony, transform.position, Quaternion.identity);
                CleaningManager.Instance.RegisterSpawnedEnemy();
                colonyStopSpawn = true;
            }
            else if (difficulty == "Medium")
            {
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                colonyStopSpawn = true;
            }
            else if (difficulty == "Hard")
            {
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                colonyStopSpawn = true;
            }
            else if (difficulty == "Nightmare")
            {
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                colonyStopSpawn = true;
            }
            else if (difficulty == "Secret")
            {
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                Instantiate(Colony, transform.position, Quaternion.identity);
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                CleaningManager.Instance.RegisterSpawnedEnemy();
                colonyStopSpawn = true;
            }
        }
    }
}