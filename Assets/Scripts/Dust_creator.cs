using System;
using UnityEngine;

public class Dust_creator : MonoBehaviour
{
    public GameObject[] enemies;

    float spawnTimer;
    float timer;
    string difficulty = ContractData.SelectedDifficulty; // Easy, Medium, Hard and Nightmare


    void Start()
    {


        spawnTimer = UnityEngine.Random.Range(5, 16);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTimer)
        {
           
            SpawnEnemy();
            timer = 0;
            spawnTimer = UnityEngine.Random.Range(5, 16);

        }



    }


    void SpawnEnemy()
    {
        if (enemies == null || enemies.Length == 0) return;
        int index = UnityEngine.Random.Range(0, enemies.Length);

        

        if (difficulty == "Easy")
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);

        }

        else if (difficulty == "Medium")
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
        }

        else if (difficulty == "Hard")
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
        }

        else if (difficulty == "Nightmare")
        {
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
            Instantiate(enemies[index], transform.position, Quaternion.identity);
        }
        

    }


    
        



}
