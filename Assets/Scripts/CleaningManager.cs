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
    public TMP_Text winRewardText;

    private int cleanedObjects = 0;
    private int totalCleanables = 0;
    public static CleaningManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cleanedObjects = 0;
        totalCleanables =
            FindObjectsByType<DirtCleanable>(FindObjectsSortMode.None).Length +
            FindObjectsByType<CleanableObject>(FindObjectsSortMode.None).Length +
            FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None).Length;

        if (totalCleanables <= 0)
        {
            Debug.LogWarning("No cleanable objects found!");
            totalCleanables = 1;
        }

        progressBar.minValue = 0;
        progressBar.maxValue = 100;
        progressBar.value = 0;

        if (progressText != null)
            progressText.text = "0% städat";

        if (winMenu != null)
            winMenu.SetActive(false);
    }

    public void RegisterSpawnedEnemy()
    {
        totalCleanables++;
        Debug.Log("Enemy spawned. Total cleanables: " + totalCleanables);
    }

    public void AddCleanedObject()
    {
        cleanedObjects++;
        float progressPercent = ((float)cleanedObjects / totalCleanables) * 100f;
        progressPercent = Mathf.Clamp(progressPercent, 0f, 100f);

        if (progressBar != null)
            progressBar.value = progressPercent;

        if (progressText != null)
            progressText.text = Mathf.RoundToInt(progressPercent) + "% städat";

        if (totalCleanables > 0 && cleanedObjects >= totalCleanables)
        {
            int rewardPerObject = 10;
            int totalRewardThisMatch = totalCleanables * rewardPerObject;

            string difficulty = ContractData.SelectedDifficulty;
            if (difficulty == "Nightmare")
                totalRewardThisMatch += 500;
            if (difficulty == "Secret")
                totalRewardThisMatch += 1000;

            if (MoneyManager.Instance != null)
                MoneyManager.Instance.AddMoney(totalRewardThisMatch);

            if (winRewardText != null)
                winRewardText.text = "+ " + totalRewardThisMatch + " $!";

            ContractManager.CompleteContract();

            if (winMenu != null)
                winMenu.SetActive(true);

            if (minimapContainer != null)
                minimapContainer.SetActive(false);

            Time.timeScale = 0f;
        }
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("ContractSelection");
    }
}