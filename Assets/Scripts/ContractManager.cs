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

    void Start()
    {
        assignedContractIndexes = new int[titleTexts.Length];
        RandomizeContracts();
    }
    public void RandomizeContracts()
    {
        int totalContracts = possibleTitles.Length;

        if (titleTexts.Length > totalContracts )
        {
            Debug.LogError("More UI slots than available contracts! Duplicates would be required");
            return;
        }

        int[] indexes = new int[totalContracts];

        for (int i = 0; i < totalContracts; i++)
        {
            indexes[i] = i;
        }

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

            assignedContractIndexes[1] = contractIndex;

            titleTexts[i].text = possibleTitles[contractIndex];

            int randomDiffIndex = Random.Range(0, possibleDiff.Length);
            diffTexts[i].text = possibleDiff[randomDiffIndex];
        }
    }

    public void LoadContractBySlot(int slotIndex)
    {
        int contractIndex = assignedContractIndexes[slotIndex];
        SceneManager.LoadScene(sceneNames[contractIndex]);
    }
}
