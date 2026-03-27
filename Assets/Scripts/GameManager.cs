using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    void Awake()
    {
        Time.timeScale = 1f;

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
        Time.timeScale = 1f;

        AudioListener.volume = PlayerPrefs.GetFloat("volume", 1f);

        bool enemiesOn = PlayerPrefs.GetInt("enemies", 1) == 1;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(enemiesOn);
        }

        Dust_creator[] dustCreators = FindObjectsByType<Dust_creator>(FindObjectsSortMode.None);
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