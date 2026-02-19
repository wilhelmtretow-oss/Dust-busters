using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContractManager : MonoBehaviour
{
    public TextMeshProUGUI[] titleTexts;
    public TextMeshProUGUI[] diffTexts;

    public string[] possibleTitles = { "Small Home", "Basic Apartment", "Large House" };
    public string[] possibleDiff = { "Easy", "Medium", "Hard" };
    public string[] sceneNames;

    private int[] assignedContractIndexes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        assignedContractIndexes = new int[titleTexts.Length];
        RandomizeContracts();
    }
    public void RandomizeContracts()
    {
        for (int i = 0; i < titleTexts.Length; i++)
        {
            int randomIndex = Random.Range(0, possibleTitles.Length);

            assignedContractIndexes[i] = randomIndex;

            titleTexts[i].text = possibleTitles[randomIndex];
            diffTexts[i].text = possibleDiff[randomIndex];
        }
    }

    public void LoadContractBySlot(int slotIndex)
    {
        int contractIndex = assignedContractIndexes[slotIndex];
        SceneManager.LoadScene(sceneNames[contractIndex]);
    }
}
