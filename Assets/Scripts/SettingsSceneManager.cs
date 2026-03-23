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
        // Load saved settings
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 1f);
        enemyToggle.isOn = PlayerPrefs.GetInt("enemies", 1) == 1;
        fullscreenToggle.isOn = Screen.fullScreen;

        ApplyVolume(volumeSlider.value);
        ToggleEnemies(enemyToggle.isOn);
    }

    public void ApplyVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }

    public void ToggleEnemies(bool enabled)
    {
        PlayerPrefs.SetInt("enemies", enabled ? 1 : 0);
    }

    public void ToggleFullscreen(bool enabled)
    {
        Screen.fullScreen = enabled;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("ContractSelection");
    }
}