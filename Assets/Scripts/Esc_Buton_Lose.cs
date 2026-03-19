using UnityEngine;

public class EscToLose : MonoBehaviour
{
    public GameObject loseCanvas;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC tryckt!");

            if (loseCanvas != null)
            {
                loseCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Debug.LogError("LoseCanvas är inte kopplad!");
            }
        }
    }
}