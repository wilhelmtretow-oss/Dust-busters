using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject modulePrefab;

    void Start()
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

        // Spawn modules into slots
        for (int i = 0; i < data.ownedModules.Count; i++)
        {
            if (i >= inventorySlots.Length) break;

            int moduleIndex = data.ownedModules[i];

            GameObject obj = Instantiate(modulePrefab, inventorySlots[i].transform);

            DraggableModule draggable = obj.GetComponent<DraggableModule>();

            if (draggable == null)
            {
                Debug.LogError("Prefab missing DraggableModule!");
                return;
            }

            draggable.moduleIndex = moduleIndex;

            RectTransform rect = obj.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector2.zero;

            inventorySlots[i].SetModule(draggable);
        }
    }
}