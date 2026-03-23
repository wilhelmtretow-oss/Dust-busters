using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;
    public int[] ownedUpgrades;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance =  this;
            DontDestroyOnLoad(gameObject);

            ownedUpgrades = new int[10];
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddUpgrade(int index)
    {
        ownedUpgrades[index]++;
    }
    
    public int GetUpgradeAmount(int index)
    {
        return ownedUpgrades[index];
    }
}
