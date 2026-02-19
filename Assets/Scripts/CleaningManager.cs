using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CleaningManager : MonoBehaviour
{
    [Header("UI")]
    public Slider progressBar;
    public Text progressText;
    public GameObject winMenu;

    private CleanableObject[] allObjects;  // Alla städbara objekt i scenen
    private int cleanedObjects = 0;

    void Start()
    {
        // Hämta alla CleanableObject i scenen
        allObjects = FindObjectsOfType<CleanableObject>();

        progressBar.minValue = 0;
        progressBar.maxValue = 100;
        progressBar.value = 0;

        if (progressText != null)
            progressText.text = "0% städat";

        if (winMenu != null)
            winMenu.SetActive(false);
    }

    // Kallas av CleanableObject efter fade
    public void AddCleanedObject()
    {
        cleanedObjects++;

        float totalObjects = allObjects.Length;

        float progressPercent = ((float)cleanedObjects / totalObjects) * 100f;

        Debug.Log($"Cleaned {cleanedObjects}/{allObjects.Length}, progress: {progressPercent}%");

        if (progressPercent > 100f)
            progressPercent = 100f;

        if (progressBar != null)
            progressBar.value = progressPercent;

        if (progressText != null)
            progressText.text = Mathf.RoundToInt(progressPercent) + "% städat";

        if (cleanedObjects >= totalObjects && winMenu != null)
            winMenu.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("ContractSelection");
    }
}
