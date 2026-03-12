using UnityEngine;

public class AddMoneyButton : MonoBehaviour
{
    public void AddContractMoney()
    {
        if (MoneyManager.Instance != null)
        {
             MoneyManager.Instance.AddMoney(500); // ContractData.SelectedMoney
             Debug.Log("Added money: 500");
        }
        else
        {
            Debug.LogError("MoneyManager not found!");
        }
       
    }
}