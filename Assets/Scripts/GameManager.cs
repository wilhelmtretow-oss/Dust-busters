using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    void Awake()
    {
        Time.timeScale = 1f; // återställ alltid timeScale vid start

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f; // återställ timeScale vid varje scenbyte

        // Applicera volym
        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1f);

        // Slå på/av enemies baserat på spelarens val
        bool enemiesOn = PlayerPrefs.GetInt("enemies", 1) == 1;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(enemiesOn);
        }

        // Slå på/av Dust_creator skript
        Dust_creator[] dustCreators = FindObjectsOfType<Dust_creator>(true);
        foreach (Dust_creator dustCreator in dustCreators)
        {
            dustCreator.enabled = enemiesOn;
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