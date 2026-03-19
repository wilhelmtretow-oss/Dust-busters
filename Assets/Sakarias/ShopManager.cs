using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI[] itemNameTexts;
    public TextMeshProUGUI[] priceTexts;
    public TextMeshProUGUI[] stockTexts;

    public string[] itemNames;
    public int[] itemPrices;
    public int[] itemStocks;

    private int shopSize;

    void Start()
    {
        shopSize = itemNameTexts.Length;
        if (ShopData.currentItemIndexes == null )
        {
            RandomizeShop();
        }
        DisplayShop();
    }
    
    void RandomizeShop()
    {
        ShopData.currentItemIndexes = new int[shopSize];
        ShopData.currentStock = new int[shopSize];

        bool[] used = new bool[itemNames.Length];

        for (int i = 0; i < shopSize; i ++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, itemNames.Length);
            }
            while (used[randomIndex]);

            used[randomIndex] = true;
            ShopData.currentItemIndexes[i] = randomIndex;

            ShopData.currentStock[i] = itemStocks[randomIndex];
        }
    }
    void DisplayShop()
    {
        for (int i = 0; i < shopSize; i++)
        {
            int index =  ShopData.currentItemIndexes[i];
            itemNameTexts[i].text = itemNames[index];
            priceTexts[i].text = itemPrices[index] + "$";
            stockTexts[i].text = ShopData.currentStock[i].ToString();
        }
    }
    public void BuyItem(int slotIndex)
    {
        int index = ShopData.currentItemIndexes[slotIndex];

        if (ShopData.currentStock[slotIndex] <= 0)
        {
            Debug.Log("Item out of stock!");
            return;
        }

        int price = itemPrices[index];

        if (MoneyManager.Instance.TotalMoney >= price)
        {
            MoneyManager.Instance.TotalMoney -= price;
            ShopData.currentStock[slotIndex]--;
            stockTexts[slotIndex].text = ShopData.currentStock[slotIndex].ToString();
            Debug.Log("Bought: " + itemNames[index]);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
    public static void ResetShop()
    {
        ShopData.currentItemIndexes = null;
    }
}
