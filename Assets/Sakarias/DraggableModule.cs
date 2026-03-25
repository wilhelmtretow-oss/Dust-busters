using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableModule : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int moduleIndex;

    [HideInInspector] public ModuleSlot currentSlot;
    [HideInInspector] public bool wasDropped;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private Transform previousParent;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousParent = transform.parent;
        wasDropped = false;

        // Use this to check if we are pulling OUT of an equipment slot
        if (currentSlot != null)
        {
            // Tell the data manager this slot is now empty IMMEDIATELY
            ModuleInventoryManager.Instance.EquipModule(currentSlot.slotIndex, -1);
            currentSlot.ClearSlot();
            currentSlot = null;
        }

        // Also clear from inventory slot references
        InventorySlot inv = previousParent.GetComponent<InventorySlot>();
        if (inv != null)
        {
            inv.ClearSlot();
        }

        canvasGroup.blocksRaycasts = false;
        // Move to the root canvas so it stays on top of everything while dragging
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (!wasDropped)
        {
            rectTransform.SetParent(previousParent);
            rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            // Destroy THIS specific dragged object because the RefreshUI 
            // scripts are about to spawn a fresh version of it from the data.
            Destroy(gameObject);

            // Trigger the global UI rebuild
            Object.FindFirstObjectByType<InventoryUIManager>()?.LoadInventory();
            Object.FindFirstObjectByType<EquipmentUIManager>()?.RefreshEquippedUI();
        }
    }
}