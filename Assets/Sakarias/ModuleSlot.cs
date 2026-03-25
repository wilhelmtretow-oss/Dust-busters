using UnityEngine;
using UnityEngine.EventSystems;

public class ModuleSlot : MonoBehaviour, IDropHandler
{
    public int slotIndex;

    private DraggableModule currentModule;

    public bool HasModule()
    {
        return currentModule != null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        DraggableModule dragged = eventData.pointerDrag.GetComponent<DraggableModule>();

        if (dragged == null || currentModule != null) return;

        // IF the item was already in an equipment slot, clear that OLD data index
        if (dragged.currentSlot != null)
        {
            ModuleInventoryManager.Instance.EquipModule(dragged.currentSlot.slotIndex, -1);
        }

        // Now set the NEW data index
        SetModule(dragged);
        ModuleInventoryManager.Instance.EquipModule(slotIndex, dragged.moduleIndex);
    }

    public void SetModule(DraggableModule module)
    {
        currentModule = module;

        module.currentSlot = this;
        module.wasDropped = true;

        RectTransform rect = module.GetComponent<RectTransform>();
        rect.SetParent(transform);
        rect.anchoredPosition = Vector2.zero;
    }

    public void LoadModuleToSlot(int moduleIndex, GameObject prefab)
    {
        if (moduleIndex == -1) return; // Slot is empty

        GameObject obj = Instantiate(prefab, transform);
        DraggableModule draggable = obj.GetComponent<DraggableModule>();

        draggable.moduleIndex = moduleIndex;
        SetModule(draggable);
    }

    public void CreateModuleInSlot(int index, GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, transform);

        DraggableModule draggable = obj.GetComponent<DraggableModule>();
        draggable.moduleIndex = index;

        SetModule(draggable);

        obj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public void ClearSlot()
    {
        currentModule = null;
    }
}