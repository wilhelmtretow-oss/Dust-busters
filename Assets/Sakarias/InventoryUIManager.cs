using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject modulePrefab;

    void OnEnable()
    {
        if (ModuleInventoryManager.Instance != null)
        {
            LoadInventory();
        }
    }

    void LoadInventory()
    {
        var data = ModuleInventoryManager.Instance;

        if (data == null||data.ownedModules == null)
        {
            Debug.LogError("Manager or List not ready!");
            return;
        }

        Debug.Log("Modules owned: " + data.ownedModules.Count);

        // Clear slots first (important if re-entering scene)
        foreach (InventorySlot slot in inventorySlots)
        {
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
            slot.ClearSlot();
        }

        int slotUIIndex = 0;

        // Spawn modules into slots
        for (int i = 0; i < data.ownedModules.Count; i++)
        {
            int moduleIndex = data.ownedModules[i];

            bool isEquiped = false;
            for (int e = 0; e < data.equippedModules.Length; e++)
            {
                if (data.equippedModules[e] == moduleIndex)
                {
                    isEquiped = true;
                    break;
                }
            }
            if (isEquiped) continue;

            if (slotUIIndex >= inventorySlots.Length) break;

            GameObject obj = Instantiate(modulePrefab, inventorySlots[slotUIIndex].transform);
            DraggableModule draggable = obj.GetComponent<DraggableModule>();

            if (draggable != null)
            {
                draggable.moduleIndex = moduleIndex;
                inventorySlots[slotUIIndex].SetModule(draggable);
            }
            slotUIIndex++;

        }
    }
}