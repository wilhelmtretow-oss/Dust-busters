using System;
using UnityEngine;

public class Colony_creator : MonoBehaviour
{
    public GameObject Colony;

    float spawnTimer;
    float timer;
    string difficulty = ContractData.SelectedDifficulty; // Easy, Medium, Hard and Nightmare


    void Start()
    {


        spawnTimer = UnityEngine.Random.Range(25, 45);
    }

    void Update()
    {
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

        if (difficulty == "Easy")
        {
            Instantiate(Colony, transform.position, Quaternion.identity);

        }

        else if (difficulty == "Medium")
        {
            Instantiate(Colony, transform.position, Quaternion.identity);
            Instantiate(Colony, transform.position, Quaternion.identity);
        }

        else if (difficulty == "Hard")
        {
            Instantiate(Colony, transform.position, Quaternion.identity);
            Instantiate(Colony, transform.position, Quaternion.identity);
            Instantiate(Colony, transform.position, Quaternion.identity);
        }

        else if (difficulty == "Nightmare")
        {
            Instantiate(Colony, transform.position, Quaternion.identity);
            Instantiate(Colony, transform.position, Quaternion.identity);
            Instantiate(Colony, transform.position, Quaternion.identity);
            Instantiate(Colony, transform.position, Quaternion.identity);
        }
        

    }


    
        



}
