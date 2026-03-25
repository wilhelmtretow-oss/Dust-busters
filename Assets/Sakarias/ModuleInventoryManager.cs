using UnityEngine;
using System.Collections.Generic;

public class ModuleInventoryManager : MonoBehaviour
{
    public static ModuleInventoryManager Instance;

    public int maxModules = 3;

    public List<int> ownedModules = new List<int>();
    public int[] equippedModules; // size = number of equip slots

    void Awake()
    {
        Debug.Log("ModuleInventoryManger awake");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        equippedModules = new int[3]; // adjust to your slot count

        for (int i = 0; i < equippedModules.Length; i++)
        {
            equippedModules[i] = -1; // empty
        }
    }

    public bool CanBuyModule()
    {
        return ownedModules.Count < maxModules;
    }

    public void AddModule(int moduleIndex)
    {
        if (!CanBuyModule())
        {
            Debug.Log("Max modules reached!");
            return;
        }

        ownedModules.Add(moduleIndex);
        Debug.Log("Module added to data: " + moduleIndex);
    }
}