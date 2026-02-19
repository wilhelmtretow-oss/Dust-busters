using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CleaningManager : MonoBehaviour
{
    [Header("UI")]
    public Slider progressBar;        // Slider i UI
    public Text progressText;         // Text ovanför baren
    public GameObject winMenu;        // WinMenu panel

    [Header("Inställningar")]
    public int totalObjectsToClean = 10; // Totalt antal objekt som ska städas

    private int cleanedObjects = 0;

    void Start()
    {
        // Initiera progressbar
        progressBar.minValue = 0;
        progressBar.maxValue = 100;  // Viktigt: måste matcha procent
        progressBar.value = 0;

        if (progressText != null)
            progressText.text = "0% städat";

        if (winMenu != null)
            winMenu.SetActive(false); // Döljer winMenu i början
    }

    // Kallas varje gång ett objekt städas
    public void AddCleanedObject()
    {
        cleanedObjects++;

        if (cleanedObjects > totalObjectsToClean)
            cleanedObjects = totalObjectsToClean;

        // Beräkna procent
        float progressPercent = ((float)cleanedObjects / totalObjectsToClean) * 100f;

        // Säkerställ att baren når exakt 100%
        if (cleanedObjects >= totalObjectsToClean)
            progressPercent = 100f;

        if (progressBar != null)
            progressBar.value = progressPercent;

        if (progressText != null)
            progressText.text = Mathf.RoundToInt(progressPercent) + "% städat";

        // Visa win-meny
        if (cleanedObjects >= totalObjectsToClean && winMenu != null)
            winMenu.SetActive(true);
    }

    // Funktion för knappen "Tillbaka till meny"
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("ContractSelection"); // Byt till din meny-scene
    }
}
