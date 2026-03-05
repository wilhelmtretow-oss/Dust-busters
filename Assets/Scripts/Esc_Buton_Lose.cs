using UnityEngine;

public class EscGameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0f; // stoppar spelet
        }
    }
}