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
        cleanedObjects = 0;

        // 🔥 RÄKNA ALLA OBJEKT DIREKT (fixar din bug)
        totalCleanables =
            FindObjectsOfType<DirtCleanable>().Length +
            FindObjectsOfType<CleanableObject>().Length +
            FindObjectsOfType<EnemyHealth>().Length;

        // Safety så det aldrig blir 0
        if (totalCleanables <= 0)
        {
            Debug.LogWarning("No cleanable objects found!");
            totalCleanables = 1;
        }

        Debug.Log("Total cleanables: " + totalCleanables);

        progressBar.minValue = 0;
        progressBar.maxValue = 100;
        progressBar.value = 0;

        if (progressText != null)
            progressText.text = "0% städat";

        if (winMenu != null)
            winMenu.SetActive(false);
    }

    public void AddCleanedObject()
    {
        cleanedObjects++;

        Debug.Log("Cleaned: " + cleanedObjects + " / " + totalCleanables);

        float progressPercent = ((float)cleanedObjects / totalCleanables) * 100f;
        progressPercent = Mathf.Clamp(progressPercent, 0f, 100f);

        if (progressBar != null)
            progressBar.value = progressPercent;

        if (progressText != null)
            progressText.text = Mathf.RoundToInt(progressPercent) + "% städat";

        // 🔥 EXTRA SÄKER CHECK
        if (totalCleanables > 0 && cleanedObjects >= totalCleanables)
        {
            if (winMenu != null)
                winMenu.SetActive(true);

            if (minimapContainer != null)
                minimapContainer.SetActive(false);

            Time.timeScale = 0f;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("ContractSelection");
    }
}