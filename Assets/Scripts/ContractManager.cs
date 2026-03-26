using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContractManager : MonoBehaviour
{
    public TextMeshProUGUI[] titleTexts;
    public TextMeshProUGUI[] diffTexts;
    public string[] possibleTitles = { "Small Home", "Basic Apartment", "Large House" };
    public string[] possibleDiff = { "Easy", "Medium", "Hard" };
    public string[] sceneNames;
    public int[] baseMoneyPerMap;
    public float[] difficultyMultipliers = { 1f, 1.5f, 2f };

    [Header("Hemlig bana")]
    public GameObject secretContractButton;
    public string secretSceneName = "masiv_manshon_test_starecasel";

    private int[] assignedContractIndexes;
    private int[] assignedDifficultyIndexes;

    void Start()
    {
        if (!PlayerPrefs.HasKey("sessionStarted"))
        {
            PlayerPrefs.SetInt("completedContracts", 0);
            PlayerPrefs.SetInt("sessionStarted", 1);
            PlayerPrefs.Save();
        }

        assignedContractIndexes = new int[titleTexts.Length];
        assignedDifficultyIndexes = new int[titleTexts.Length];
        RandomizeContracts();

        // Visa/göm hemlig bana baserat på antal klarade banor
        int completedContracts = PlayerPrefs.GetInt("completedContracts", 0);
        if (secretContractButton != null)
            secretContractButton.SetActive(completedContracts >= 5);
    }

    public void RandomizeContracts()
    {
        int totalContracts = possibleTitles.Length;
        if (titleTexts.Length > totalContracts)
        {
            Debug.LogError("More UI slots than available contracts! Duplicates would be required");
            return;
        }

        int[] indexes = new int[totalContracts];
        for (int i = 0; i < totalContracts; i++)
            indexes[i] = i;

        for (int i = 0; i < totalContracts; i++)
        {
            int randomIndex = Random.Range(i, totalContracts);
            int temp = indexes[i];
            indexes[i] = indexes[randomIndex];
            indexes[randomIndex] = temp;
        }

        for (int i = 0; i < titleTexts.Length; i++)
        {
            int contractIndex = indexes[i];
            assignedContractIndexes[i] = contractIndex;
            titleTexts[i].text = possibleTitles[contractIndex];

            int randomDiffIndex = Random.Range(0, possibleDiff.Length);
            assignedDifficultyIndexes[i] = randomDiffIndex;
            diffTexts[i].text = possibleDiff[randomDiffIndex];
        }
    }

    public void LoadContractBySlot(int slotIndex)
    {
        int contractIndex = assignedContractIndexes[slotIndex];
        int diffIndex = assignedDifficultyIndexes[slotIndex];

        int baseMoney = baseMoneyPerMap[contractIndex];
        float multiplier = difficultyMultipliers[diffIndex];
        int finalmoney = Mathf.RoundToInt(baseMoney * multiplier);

        ContractData.SelectedDifficulty = possibleDiff[diffIndex];
        ContractData.SelectedMoney = finalmoney;

        SceneManager.LoadScene(sceneNames[contractIndex]);
    }

    public void LoadSecretContract()
    {
        ContractData.SelectedDifficulty = "Secret";

        // RESET så man måste klara 5 igen
        PlayerPrefs.SetInt("completedContracts", 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene(secretSceneName);
    }

    // Anropa denna när spelaren klarar en bana
    public static void CompleteContract()
    {
        int completed = PlayerPrefs.GetInt("completedContracts", 0);
        completed++;

        PlayerPrefs.SetInt("completedContracts", completed);
        PlayerPrefs.Save();

        Debug.Log("Klarade banor: " + completed);
    }
}