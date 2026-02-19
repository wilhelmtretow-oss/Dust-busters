using UnityEngine;

public class Trigger : MonoBehaviour
{
    public string collidingTag = "Player";
    public GameObject monsterPrefab;
    public float spawnOffsetX = 2f;
    public string parentObjectName = "Enemy_nev";

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(collidingTag) && !hasSpawned)
        {
            hasSpawned = true;

            // Spawn position lite till höger
            Vector3 spawnPosition = transform.position + new Vector3(spawnOffsetX, 0f, 0f);

            // Skapa enemy
            GameObject newEnemy = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

            // Hitta parent i scenen
            GameObject parentObject = GameObject.Find(parentObjectName);

            if (parentObject != null)
            {
                newEnemy.transform.SetParent(parentObject.transform);
            }
            else
            {
                Debug.LogWarning("Enemy_nev hittades inte i scenen!");
            }
        }
    }
}
