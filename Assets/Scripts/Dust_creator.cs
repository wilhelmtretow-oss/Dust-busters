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
        spawnTimer = UnityEngine.Random.Range(15, 25); // Set time threashold between 15 to 25 seconds
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTimer) // if the timer has supassed threashold spawn play script
        {
           
            SpawnEnemy();
            timer = 0;
            spawnTimer = UnityEngine.Random.Range(20, 25); // Increase the threashold needed to spawn
            enemyAmount += 1; // remembers how many spawns it did
        }



    }


    void SpawnEnemy()
    {
        if (enemies == null || enemies.Length == 0) return;
        int index = UnityEngine.Random.Range(0, enemies.Length);



        if (difficulty == "Easy" && enemyAmount <= 2) // if easy difficulty and has spawned 2 or less waves play script
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity); // check enemies in the index and place on top of self

        }

        else if (difficulty == "Medium" && enemyAmount <= 2) // if medium difficulty and has spawned 2 or less waves play script
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);
        }

        else if (difficulty == "Hard" && enemyAmount <= 2) // if hard difficulty and has spawned 2 or less waves play script
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
        }

        else if (difficulty == "Nightmare" && enemyAmount <= 2) // if nightmare difficulty and has spawned 2 or less waves play script
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
        }

        else if (difficulty == "Secret" && enemyAmount <= 2) // if secret difficulty and has spawned 2 or less waves play script
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
        }


    }







}
