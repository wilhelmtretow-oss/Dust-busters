using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CleaningManager : MonoBehaviour
{
    [Header("UI")]
    public Slider progressBar;
    public TMP_Text progressText;
    public GameObject winMenu;
    public GameObject minimapContainer;

    private int cleanedObjects = 0;
    private int totalCleanables = 0;

    void Start()
    {
        if (totalCleanables == 0)
        {
            Debug.LogWarning("No cleanable objects or enemies found!");
            totalCleanables = 1;
        }

        // Hämta alla CleanableObject och EnemyHealth i scenen
        CleanableObject[] cleanables = FindObjectsOfType<CleanableObject>();
        EnemyHealth[] enemies = FindObjectsOfType<EnemyHealth>();

        totalCleanables = cleanables.Length + enemies.Length; // total antal objekt + fiender

        progressBar.minValue = 0;
        progressBar.maxValue = 100;
        progressBar.value = 0;

        if (progressText != null)
            progressText.text = "0% städat";

        if (winMenu != null)
            winMenu.SetActive(false);
    }

    // Kallas när ett objekt eller fiende blir städat/död
    public void AddCleanedObject()
    {
        cleanedObjects++;

        float progressPercent = ((float)cleanedObjects / totalCleanables) * 100f;
        if (progressPercent > 100f) progressPercent = 100f;

        if (progressBar != null)
            progressBar.value = progressPercent;

        if (progressText != null)
            progressText.text = Mathf.RoundToInt(progressPercent) + "% städat";

        if (cleanedObjects >= totalCleanables && winMenu != null)
        {
            winMenu.SetActive(true);

            if (minimapContainer != null)
                minimapContainer.SetActive(false);

            Time.timeScale = 0f; // Pausar spelet 
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("ContractSelection");
    }
}