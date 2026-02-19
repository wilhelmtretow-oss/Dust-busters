using UnityEngine;
using UnityEngine.UI;

public class CleaningManager : MonoBehaviour
{
    public Slider progressBar;
    public int totalObjectsToClean = 5;

    private int cleanedObjects = 0;

    public void AddCleanProgress()
    {
        cleanedObjects++;

        float progress = (float)cleanedObjects / totalObjectsToClean;
        progressBar.value = progress * 100f;

        if (cleanedObjects >= totalObjectsToClean)
        {
            Debug.Log("Allt är städat!");
        }
    }
}
