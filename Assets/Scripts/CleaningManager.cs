using UnityEngine;
using UnityEngine.UI;

public class CleaningManager : MonoBehaviour
{
    public Slider progressBar;        // Progressbaren
    public Text progressText;         // Text ovanför baren

    public int totalObjectsToClean = 10;  // Totalt antal objekt i scenen
    private int cleanedObjects = 0;       // Räknar hur många som städats

    void Start()
    {
        progressBar.minValue = 0;
        progressBar.maxValue = 100;
        progressBar.value = 0;

        if (progressText != null)
            progressText.text = "0% städat";
    }

    public void AddCleanedObject()
    {
        cleanedObjects++;

        if (cleanedObjects > totalObjectsToClean)
            cleanedObjects = totalObjectsToClean;

        // Beräkna procent
        float progressPercent = ((float)cleanedObjects / totalObjectsToClean) * 100f;
        progressBar.value = progressPercent;

        if (progressText != null)
            progressText.text = Mathf.RoundToInt(progressPercent) + "% städat";

        if (cleanedObjects >= totalObjectsToClean)
        {
            Debug.Log("Allt är städat! 🎉");
        }
    }
}
