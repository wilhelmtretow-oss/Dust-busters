using System;
using UnityEngine;

public class Dust_creator : MonoBehaviour
{
    public GameObject[] enemies;

    float spawnTimer;
    float timer;
    string difficulty = ContractData.SelectedDifficulty; // Easy, Medium, Hard and Nightmare
    float enemyAmount = 0;


    void Start()
    {
        spawnTimer = UnityEngine.Random.Range(15, 25);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTimer)
        {
           
            SpawnEnemy();
            timer = 0;
            spawnTimer = UnityEngine.Random.Range(20, 25);
            enemyAmount += 1;
        }



    }


    void SpawnEnemy()
    {
        if (enemies == null || enemies.Length == 0) return;
        int index = UnityEngine.Random.Range(0, enemies.Length);

        

        if (difficulty == "Easy" && enemyAmount <= 2)
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);

        }

        else if (difficulty == "Medium" && enemyAmount <= 2)
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);
        }

        else if (difficulty == "Hard" && enemyAmount <= 2)
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
        }

        else if (difficulty == "Nightmare" && enemyAmount <= 2)
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
        }
        

    }


    
        



}
