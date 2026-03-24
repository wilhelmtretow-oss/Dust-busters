using UnityEngine;

public class ModuleManager : MonoBehaviour
{
    public static ModuleManager Instance;

    public int[] ownedModules;
    public int[] equippedModules;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            ownedModules = new int[50];
            equippedModules = new int[3];
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void BuyModule(int index)
    {
        ownedModules[index]++;
    }
    public void EquipModule(int slotIndex, int moduleIndex)
    {
        if (ownedModules[moduleIndex]>0)
        {
            equippedModules[slotIndex] = moduleIndex;
            Debug.Log("Equipped module" + moduleIndex + " in slot " + slotIndex);
        }
        else
        {
            Debug.Log("Module is not owned!");
        }
        
    }
    public int GetEquipedModule(int slotIndex)
    {
        return equippedModules[slotIndex];
    }
}
