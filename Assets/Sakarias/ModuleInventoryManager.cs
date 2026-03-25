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
        if (moduleIndex == -1) // Unequipping
        {
            equippedModules[slotIndex] = -1;
            return;
        }

        // The Fix: Check the actual owned list
        if (ownedModules.Contains(moduleIndex))
        {
            equippedModules[slotIndex] = moduleIndex;
            Debug.Log($"Equipped {moduleIndex} to slot {slotIndex}");
        }
        else
        {
            Debug.LogError("System Error: Attempted to equip unowned module!");
        }
    }
}