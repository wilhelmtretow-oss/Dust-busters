using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f; // Starta tiden igen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}