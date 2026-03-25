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
        if (data == null) return;

        for (int i = 0; i < data.equippedModules.Length; i++)
        {
            // 1. Safety check: make sure we have a UI slot for this data index
            if (i >= equipSlots.Length) break;

            // 2. Clear the physical slot completely before spawning
            // This prevents the "Double Spawn" if OnEnable runs twice
            foreach (Transform child in equipSlots[i].transform)
            {
                Destroy(child.gameObject);
            }
            equipSlots[i].ClearSlot();

            // 3. Get the saved ID
            int savedModuleID = data.equippedModules[i];

            // 4. If the ID isn't -1, spawn the icon
            if (savedModuleID != -1)
            {
                // Use the method we added to ModuleSlot earlier
                equipSlots[i].CreateModuleInSlot(savedModuleID, modulePrefab);
            }
        }
    }
}

