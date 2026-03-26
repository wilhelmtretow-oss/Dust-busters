using UnityEngine;

public class ModuleDatabase : MonoBehaviour
{
    public static ModuleDatabase instance;

    public ModuleData[] modules;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public ModuleData GetModuleByID(int id)
    {
        foreach (ModuleData data in modules)
        {
            if (data.moduleID == id) return data;
        }
        Debug.LogError($"Module ID {id} not found in database!");
        return null;
    }
}
