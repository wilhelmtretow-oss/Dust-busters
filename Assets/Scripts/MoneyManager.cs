using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        if (moneyText == null)
        {
            GameObject foundObj = GameObject.Find("MoneyTextAmount");
            if (foundObj != null)
            {
                moneyText = foundObj.GetComponent<TMP_Text>();
                UpdateMoneyUI();
            }
        }
    }

    public void AddMoney(int amount)
    {
        TotalMoney += amount;
        SaveMoney();
        UpdateMoneyUI();
        Debug.Log("Pengar sparade! Totalt: " + TotalMoney);
    }

    void SaveMoney()
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
            moneyText.text = "Pengar: " + TotalMoney + " kr";
    }

    [ContextMenu("Reset Money")]
    public void ResetMoney()
    {
        PlayerPrefs.DeleteKey("SavedMoney");
        TotalMoney = 0;
        UpdateMoneyUI();
    }
}