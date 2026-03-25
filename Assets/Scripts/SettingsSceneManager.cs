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
        // Ladda sparade inställningar, standardvärden om inget är sparat
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f); // 1f = 100%
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
    }

    public void ToggleFullscreen(bool enabled)
    {
        Screen.fullScreen = enabled;
        PlayerPrefs.SetInt("fullscreen", enabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        // Spara alla inställningar när spelet stängs
        PlayerPrefs.Save();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("ContractSelection");
    }
}