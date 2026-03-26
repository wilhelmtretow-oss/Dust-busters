using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Added for easy list checking

public class ModuleInventoryManager : MonoBehaviour
{
    public static ModuleInventoryManager Instance;

    [Header("Settings")]
    public int maxInventorySlots = 50;
    public int equipSlotCount = 3;

    [Header("Current Data")]
    // The list of every module ID the player has bought
    public List<int> ownedModules = new List<int>();

    // The IDs of modules currently in the 3 active slots (-1 = empty)
    public int[] equippedModules;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize equipment slots as empty
        equippedModules = new int[equipSlotCount];
        for (int i = 0; i < equippedModules.Length; i++)
        {
            equippedModules[i] = -1;
        }
    }

    // --- SHOP ACTIONS ---
    public bool CanBuyModule() => ownedModules.Count < maxInventorySlots;

    public void AddModule(int moduleIndex)
    {
        if (CanBuyModule())
        {
            ownedModules.Add(moduleIndex);
            Debug.Log($"Module {moduleIndex} added to Inventory.");
        }
    }

    // --- GAMEPLAY / UI ACTIONS ---
    public void EquipModule(int slotIndex, int moduleIndex)
    {
        if (moduleIndex == -1)
        {
            equippedModules[slotIndex] = -1;
            return;
        }

        // 1. CLEAN UP: If this module is already in any other slot, clear it!
        for (int i = 0; i < equippedModules.Length; i++)
        {
            if (equippedModules[i] == moduleIndex)
            {
                equippedModules[i] = -1;
            }
        }

        // 2. SET: Now put it in the new slot safely
        equippedModules[slotIndex] = moduleIndex;
        Debug.Log($"Module {moduleIndex} moved to slot {slotIndex}");
    }
    public float GetTotalBonus(string statType)
    {
        float total = 0;

        if (equippedModules == null) return 0;

        foreach (int id in equippedModules)
        {
            if (id == -1) continue;

            ModuleData data = ModuleDatabase.instance.GetModuleByID(id);

            if (data != null)
            {
                switch (statType)
                {
                    case "Damage": total += data.damageBonus; break;
                    case "Defence": total += data.defenceBonus; break;
                    case "Speed": total += data.speedBonus; break;
                }
            }
        }
        return total;
    }
}