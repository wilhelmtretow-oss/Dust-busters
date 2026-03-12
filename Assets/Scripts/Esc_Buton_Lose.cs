using UnityEngine;

public class EscGameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject minimapCanvas;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Om minimapen är aktiv gör inget
            if (minimapCanvas.activeSelf)
            {
                return;
            }

            // Annars visa game over
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}