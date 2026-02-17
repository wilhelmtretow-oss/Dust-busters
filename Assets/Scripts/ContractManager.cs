using TMPro;
using UnityEngine;

public class ContractManager : MonoBehaviour
{
    public TextMeshProUGUI[] titleTexts;
    public TextMeshProUGUI[] diffTexts;

    public string[] possibleTitles = { "Small Home", "Basic Apartment", "Large House" };
    public string[] possibleDiff = { "Easy", "Medium", "Hard" };
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomizeContracts();
    }
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    public void RandomizeContracts()
    {
        for (int i = 0; i < titleTexts.Length; i++)
        {
            int randomTitleIndex = Random.Range(0, possibleTitles.Length);
            int randomDiffIndex = Random.Range(0, possibleDiff.Length);
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
            titleTexts[i].text = possibleTitles[randomTitleIndex];
            diffTexts[i].text = possibleDiff[randomDiffIndex];
        }
    }
<<<<<<< Updated upstream
}
=======

}
>>>>>>> Stashed changes
