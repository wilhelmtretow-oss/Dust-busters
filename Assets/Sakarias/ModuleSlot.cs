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
        if (dragged == null) return;

        if (currentModule != null)
        {
            Debug.Log("Slot occupied!");
            return;
        }

        SetModule(dragged);
        ModuleManager.Instance.EquipModule(slotIndex, dragged.moduleIndex);
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

    public void ClearSlot()
    {
        currentModule = null;
    }
}