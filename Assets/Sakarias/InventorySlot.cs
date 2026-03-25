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
        if (dragged == null) return;

        if (currentModule != null)
        {
            Debug.Log("Inventory full!");
            return;
        }

        SetModule(dragged);
    }
}