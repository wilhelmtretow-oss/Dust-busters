using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // This function now asks: "Which object should I turn off?"
    public void DisableSpecificButton(GameObject target)
    {
        if (target != null)
        {
            target.SetActive(false);
        }
    }
}