using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModuleSlot : MonoBehaviour, IDropHandler
{
    public int slotIndex;
    private DraggableModule currentModule;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        DraggableModule dragged = eventData.pointerDrag.GetComponent<DraggableModule>();

        if (dragged != null && currentModule == null)
        {
            ModuleInventoryManager.Instance.EquipModule(slotIndex, dragged.moduleIndex);
            SetModule(dragged);
        }
    }

    public void SetModule(DraggableModule module)
    {
        currentModule = module;
        module.currentSlot = this;
        module.wasDropped = true;

        RectTransform rect = module.GetComponent<RectTransform>();

        // 1. Byt förälder utan att spara gamla world-positioner
        rect.SetParent(transform, false);

        // 2. Tvinga anchors till stretch (om du vill att den ska fylla slotten)
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;

        // 3. DETTA ÄR VIKTIGAST: Nollställ marginalerna som spökade i din bild
        rect.offsetMin = Vector2.zero; // Sätter Left och Bottom till 0
        rect.offsetMax = Vector2.zero; // Sätter Right och Top till 0

        // 4. Dubbelkolla skala och Z
        rect.localScale = Vector3.one;
        rect.localPosition = Vector3.zero;
    }

    public void LoadModuleToSlot(int moduleIndex, GameObject prefab)
    {
        if (moduleIndex == -1) return;

        // Spawn direkt som barn till slotten istället för att gå via Canvas (säkrare)
        GameObject obj = Instantiate(prefab, transform);
        obj.transform.localScale = Vector3.one; // Dubbelkoll

        DraggableModule draggable = obj.GetComponent<DraggableModule>();
        draggable.moduleIndex = moduleIndex;

        ModuleData info = ModuleDatabase.instance.GetModuleByID(moduleIndex);
        if (info != null)
        {
            Transform iconChild = obj.transform.Find("Icon");
            if (iconChild != null)
            {
                Image img = iconChild.GetComponent<Image>();
                img.sprite = info.moduleIcon;
                img.color = Color.white; // Se till att den inte är genomskinlig
            }
        }

        SetModule(draggable);
    }

    public void ClearSlot() => currentModule = null;
}