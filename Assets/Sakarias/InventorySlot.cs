using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableModule draggable = eventData.pointerDrag.GetComponent<DraggableModule>();

        if (draggable != null)
        {
            RectTransform draggedRect = draggable.GetComponent<RectTransform>();
            RectTransform slotRect = GetComponent<RectTransform>();

            draggedRect.SetParent(slotRect);
            draggedRect.anchoredPosition = Vector2.zero;

            Debug.Log("Module returned to inventory");
        }
    }
}
