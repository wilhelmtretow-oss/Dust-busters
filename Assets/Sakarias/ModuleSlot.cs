using UnityEngine;
using UnityEngine.EventSystems;

public class ModuleSlot : MonoBehaviour, IDropHandler
{
    public int slotIndex;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        DraggableModule draggable = eventData.pointerDrag.GetComponent<DraggableModule>();

        if (draggable != null)
        {
            int moduleIndex = draggable.moduleIndex;

            Debug.Log("Dropped module " + moduleIndex + " into slot " + slotIndex);

            ModuleManager.Instance.EquipModule(slotIndex, moduleIndex);

            RectTransform draggedRect = draggable.GetComponent<RectTransform>();
            RectTransform slotRect = GetComponent<RectTransform>();

            draggedRect.SetParent(slotRect);
            draggedRect.anchoredPosition = Vector2.zero;
            
        }
    }
}