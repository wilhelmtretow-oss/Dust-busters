using UnityEngine;

public class EquipmentUIManager : MonoBehaviour
{
    public ModuleSlot[] equipSlots;
    public GameObject modulePrefab;

    private void OnEnable()
    {
        RefreshEquippedUI();
    }
    public void RefreshEquippedUI()
    {
        var data = ModuleInventoryManager.Instance;
        if (data == null )
        {
            return;
        }

        foreach (var slot in equipSlots)
        {
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
            slot.ClearSlot();
        }
        for (int i = 0; i < data.equippedModules.Length; i++)
        {
            if (i < equipSlots.Length)
            {
                int savedModuleID = data.equippedModules[i];
                if (savedModuleID != -1)
                {
                    {
                        equipSlots[i].LoadModuleToSlot(savedModuleID, modulePrefab);
                    }
                }
            }
        }
    }
}
