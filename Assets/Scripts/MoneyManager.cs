using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// this is the script that manages the player's money.
// It keeps track of how much money the player has, updates the UI, and handles saving/loading the money amount to disk.
// It also provides a method for trying to make a purchase, which can be called from the shop manager when the player tries to buy something.
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public int TotalMoney = 0;
    public TMP_Text moneyText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadMoney();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Vi letar efter texten varje gång en ny scen laddas
        GameObject foundObj = GameObject.Find("MoneyTextAmount");
        if (foundObj != null)
        {
            moneyText = foundObj.GetComponent<TMP_Text>();
            UpdateMoneyUI();
        }
    }

    
    public bool TryPurchase(int cost)
    {
        if (TotalMoney >= cost)
        {
            TotalMoney -= cost; // Dra av pengarna
            SaveMoney();        // Spara nya summan till hårddisken direkt
            UpdateMoneyUI();    // Uppdatera texten i UI:t
            Debug.Log("Köp genomfört! Kvarvarande pengar: " + TotalMoney);
            return true;        // Berätta för shoppen att det gick bra
        }
        else
        {
            Debug.Log("För lite pengar för att köpa detta.");
            return false;       // Berätta för shoppen att det misslyckades
        }
    }

    public void AddMoney(int amount)
    {
        TotalMoney += amount;
        SaveMoney();
        UpdateMoneyUI();
        Debug.Log("Pengar sparade! Totalt: " + TotalMoney);
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("SavedMoney", TotalMoney);
        PlayerPrefs.Save();
    }

    void LoadMoney()
    {
        TotalMoney = PlayerPrefs.GetInt("SavedMoney", 0);
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            // Här kan du ändra "$" till "kr" om du vill ha svenska
            moneyText.text = "Money: " + TotalMoney + " $";
        }
    }

    [ContextMenu("Reset Money")]
    public void ResetMoney()
    {
        PlayerPrefs.DeleteKey("SavedMoney");
        TotalMoney = 0;
        UpdateMoneyUI();
    }
}