using UnityEngine;
using UnityEngine.UI;

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

    public void LoadInventory()
    {
        var data = ModuleInventoryManager.Instance;
        if (data == null || data.ownedModules == null) return;

        // Rensa gamla objekt
        foreach (InventorySlot slot in inventorySlots)
        {
            foreach (Transform child in slot.transform) { Destroy(child.gameObject); }
            slot.ClearSlot();
        }

        int slotUIIndex = 0;
        for (int i = 0; i < data.ownedModules.Count; i++)
        {
            int moduleID = data.ownedModules[i];

            // Kolla om den redan är utrustad
            bool isEquipped = false;
            foreach (int eqID in data.equippedModules) { if (eqID == moduleID) isEquipped = true; }
            if (isEquipped) continue;

            if (slotUIIndex >= inventorySlots.Length) break;

            // SPAWNA DIREKT I SLOTTEN
            GameObject obj = Instantiate(modulePrefab, inventorySlots[slotUIIndex].transform);

            // Säkerställ synlighet
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.localPosition = Vector3.zero;
            rt.localScale = Vector3.one;

            ModuleData info = ModuleDatabase.instance.GetModuleByID(moduleID);
            if (info != null)
            {
                Transform iconChild = obj.transform.Find("Icon");
                if (iconChild != null)
                {
                    Image img = iconChild.GetComponent<Image>();
                    img.sprite = info.moduleIcon;
                    img.color = Color.white;
                }
                obj.name = "Module_" + info.moduleName;
            }

            DraggableModule draggable = obj.GetComponent<DraggableModule>();
            draggable.moduleIndex = moduleID;

            inventorySlots[slotUIIndex].SetModule(draggable);
            slotUIIndex++;
        }
    }
}