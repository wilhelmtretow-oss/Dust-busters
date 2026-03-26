using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetData : MonoBehaviour
{
    public void ResetAllGameData()
    {
        // 1. Nollställ pengar i MoneyManager (om den finns i scenen)
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.TotalMoney = 0;
            MoneyManager.Instance.UpdateMoneyUI();
        }

        // 2. Radera ALLA sparade nycklar från hårddisken (PlayerPrefs)
        // Detta tar bort "SavedMoney", "completedContracts" och allt annat du sparat
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // 3. Nollställ sessions-trackern så att den hemliga banan låses direkt
        SessionTracker.IsInitialized = false;

        Debug.Log("ALL DATA HAR BLIVIT NOLLSTÄLLD!");

        // 4. Ladda om nuvarande scen för att uppdatera alla texter och knappar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}