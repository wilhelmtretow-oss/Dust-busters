using UnityEngine;

public class BlockAnyKeyMouse : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(HidePanel), 0.5f);
    }

    void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
