using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private DraggableModule currentModule;

    public bool HasModule()
    {
        return currentModule != null;
    }

    public void SetModule(DraggableModule module)
    {
        currentModule = module;

        module.wasDropped = true;
        module.currentSlot = null;

        RectTransform rect = module.GetComponent<RectTransform>();
        rect.SetParent(transform);
        rect.anchoredPosition = Vector2.zero;
    }

    public void ClearSlot()
    {
        currentModule = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        DraggableModule dragged = eventData.pointerDrag.GetComponent<DraggableModule>();

        if (dragged == null || currentModule != null) return;

        // The data for the equipment slot was already cleared in OnBeginDrag,
        // so we just need to "Accept" the drop here.
        dragged.wasDropped = true;

        // We don't even need to call SetModule(dragged) here because 
        // LoadInventory() is going to wipe the UI and rebuild it anyway!
        Debug.Log("Inventory accepted drop, rebuilding UI...");
    }
}