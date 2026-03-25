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

        // Clear from equipped slot
        if (currentSlot != null)
        {
            currentSlot.ClearSlot();
            currentSlot = null;
        }

        // Clear from inventory slot
        InventorySlot inv = previousParent.GetComponent<InventorySlot>();
        if (inv != null)
        {
            inv.ClearSlot();
        }

        canvasGroup.blocksRaycasts = false;
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
    }
}