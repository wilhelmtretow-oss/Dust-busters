using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    private int currentMoney = 0;
    public TMP_Text moneyText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // LADDA PENGARNA NÄR SPELET STARTAR
            LoadMoney();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;

        // SPARA PENGARNA DIREKT NÄR DE ÄNDRAS
        SaveMoney();

        UpdateMoneyUI();
        Debug.Log("Pengar sparade! Totalt: " + currentMoney);
    }

    // Sparar till hårddisken
    void SaveMoney()
    {
        PlayerPrefs.SetInt("SavedMoney", currentMoney);
        PlayerPrefs.Save(); // Tvingar Unity att skriva ner det till filen direkt
    }

    // Laddar från hårddisken
    void LoadMoney()
    {
        // Om "SavedMoney" inte finns (första gången man spelar), sätts det till 0
        currentMoney = PlayerPrefs.GetInt("SavedMoney", 0);
        UpdateMoneyUI();
    }

    void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = "Pengar: " + currentMoney + " kr";
    }

    // En fuskkod för dig om du vill nollställa pengarna under testning
    [ContextMenu("Reset Money")]
    public void ResetMoney()
    {
        PlayerPrefs.DeleteKey("SavedMoney");
        currentMoney = 0;
        UpdateMoneyUI();
    }
}