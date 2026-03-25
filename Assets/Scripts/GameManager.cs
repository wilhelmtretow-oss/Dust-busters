using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        bool enemiesOn = PlayerPrefs.GetInt("enemies", 1) == 1;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(enemiesOn);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToContracts()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ContractSelection");
    }

    public void GoToSettings()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SettingsScene");
    }
}