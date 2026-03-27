using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI[] itemNameTexts;
    public TextMeshProUGUI[] priceTexts;
    public TextMeshProUGUI[] stockTexts;
    public Button[] buyButtons;

    public TextMeshProUGUI PurchaseText;
    public CanvasGroup purchaseCanvasGroup;

    public string[] itemNames;
    public int[] itemPrices;
    public int[] itemStocks;

    private int shopSize;
    private int[] purchaseCounts;

    private Coroutine fadeCoroutine;

    void Start()
    {
        shopSize = itemNameTexts.Length;
        purchaseCounts = new int[itemNames.Length];
        if (ShopData.currentItemIndexes == null)
        {
            RandomizeShop();
        }
        DisplayShop();
        StartCoroutine(FixButtonStates());
    }

    private void Update()
    {
        UpdateButtonStates();
    }

    void RandomizeShop()
    {
        ShopData.currentItemIndexes = new int[shopSize];
        ShopData.currentStock = new int[shopSize];

        bool[] used = new bool[itemNames.Length];

        for (int i = 0; i < shopSize; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, itemNames.Length);
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
            int index = ShopData.currentItemIndexes[i];
            itemNameTexts[i].text = itemNames[index];
            priceTexts[i].text = itemPrices[index] + " $"; 
            stockTexts[i].text = ShopData.currentStock[i].ToString();
        }
    }

    void UpdateButtonStates()
    {
        if (MoneyManager.Instance == null) return;

        for (int i = 0; i < shopSize; i++)
        {
            int index = ShopData.currentItemIndexes[i];

            bool hasStock = ShopData.currentStock[i] > 0;
            bool canAfford = MoneyManager.Instance.TotalMoney >= itemPrices[index];

            buyButtons[i].interactable = hasStock && canAfford;
        }
    }

    public void BuyItem(int slotIndex)
    {
        int index = ShopData.currentItemIndexes[slotIndex];

        // 1. Kolla lager först
        if (ShopData.currentStock[slotIndex] <= 0)
        {
            Debug.Log("Slut i lager!");
            return;
        }

        int price = itemPrices[index];

        // 2. Använd TryPurchase för att dra pengar, uppdatera UI och SPARA automatiskt
        if (MoneyManager.Instance.TryPurchase(price))
        {
            

            if (index <= 6)
            {
                UpgradeManager.Instance.AddUpgrade(index);
                Debug.Log("Köpte uppgradering: " + index);
            }
            else
            {
                int moduleIndex = index - 7;

                if (!ModuleInventoryManager.Instance.CanBuyModule())
                {
                    // Om vi inte kan köpa modulen, ge tillbaka pengarna!
                    MoneyManager.Instance.AddMoney(price);
                    Debug.Log("För många moduler! Pengar återbetalda.");
                    return;
                }

                ModuleInventoryManager.Instance.AddModule(moduleIndex);
            }

            // Minska lagret
            ShopData.currentStock[slotIndex]--;
            stockTexts[slotIndex].text = ShopData.currentStock[slotIndex].ToString();

            purchaseCounts[index]++;
            ShowPurchaseText(index);

            if (ShopData.currentStock[slotIndex] <= 0)
            {
                buyButtons[slotIndex].interactable = false;
            }
            Debug.Log("Köpt: " + itemNames[index]);
        }
        else
        {
            // Detta körs om TryPurchase returnerar false (för lite pengar)
            Debug.Log("Du har inte råd!");
        }
    }

    void ShowPurchaseText(int index)
    {
        string itemName = itemNames[index];
        int count = purchaseCounts[index];

        PurchaseText.text = "Bought " + itemName + " " + count + "X";
        purchaseCanvasGroup.alpha = 1f;

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = StartCoroutine(FadePurchaseText());
    }

    IEnumerator FixButtonStates()
    {
        yield return null;
        for (int i = 0; i < shopSize; i++)
        {
            int index = ShopData.currentItemIndexes[i];
            bool canAfford = MoneyManager.Instance.TotalMoney >= itemPrices[index];
            buyButtons[i].interactable = ShopData.currentStock[i] > 0 && canAfford;
        }
    }

    IEnumerator FadePurchaseText()
    {
        yield return new WaitForSeconds(2f);
        float duration = 1.5f;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            purchaseCanvasGroup.alpha = Mathf.Lerp(1f, 0f, time / duration);
            yield return null;
        }
        purchaseCanvasGroup.alpha = 0f;
    }

    public static void ResetShop()
    {
        ShopData.currentItemIndexes = null;
        ShopData.currentStock = null;
    }
}