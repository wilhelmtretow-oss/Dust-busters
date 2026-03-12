using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    void Update()
    {
        if (MoneyManager.Instance != null)
        {
            moneyText.text = MoneyManager.Instance.TotalMoney.ToString();
        }
    }
}
