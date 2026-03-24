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
    public ModuleData GetModule(int index)
    {
        return modules[index];
    }
}
