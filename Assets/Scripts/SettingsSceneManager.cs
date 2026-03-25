using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsSceneManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle enemyToggle;
    public Toggle fullscreenToggle;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        enemyToggle.isOn = PlayerPrefs.GetInt("enemies", 1) == 1;
        fullscreenToggle.isOn = PlayerPrefs.GetInt("fullscreen", Screen.fullScreen ? 1 : 0) == 1;

        ApplyVolume(volumeSlider.value);
        ToggleEnemies(enemyToggle.isOn);
        ToggleFullscreen(fullscreenToggle.isOn);
    }

    public void ApplyVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
        PlayerPrefs.Save();
    }

    public void ToggleEnemies(bool enabled)
    {
        PlayerPrefs.SetInt("enemies", enabled ? 1 : 0);
        PlayerPrefs.Save();

        // Aktivera/inaktivera alla fiender i nuvarande scen
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(enabled);
        }
    }

    public void ToggleFullscreen(bool enabled)
    {
        Screen.fullScreen = enabled;
        PlayerPrefs.SetInt("fullscreen", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("ContractSelection");
    }
}