using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private DraggableModule currentModule;

    public bool HasModule() => currentModule != null;

    public void SetModule(DraggableModule module)
    {
        currentModule = module;
        module.wasDropped = true;
        module.currentSlot = null;

        RectTransform rect = module.GetComponent<RectTransform>();

        // VIKTIGT: 'false' ser till att den inte ärver konstiga world-positions
        rect.SetParent(transform, false);

        // Nollställ allt för att tvinga fram synlighet
        rect.anchoredPosition = Vector2.zero;
        rect.localScale = Vector3.one;
        rect.localPosition = new Vector3(0, 0, 0);
    }

    public void ClearSlot() => currentModule = null;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        DraggableModule dragged = eventData.pointerDrag.GetComponent<DraggableModule>();

        if (dragged == null || currentModule != null) return;

        dragged.wasDropped = true;
        Debug.Log("Inventory accepted drop.");
        // Tips: Du kan anropa SetModule(dragged) här om du vill se den landa direkt
    }
}