using UnityEngine;

public class BlockAnyKeyMouse : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(HidePanel), 0.5f); // When the game starts bring a panal to stop any inputs
    }

    void HidePanel()
    {
        gameObject.SetActive(false); // hide panal
    }
}
