using UnityEngine;
using UnityEngine.UI;

public class MinimapToggle : MonoBehaviour
{
    public GameObject minimapSmall;
    public GameObject minimapFull;

    public void ToggleMinimap()
    {
        bool isSmallActive = minimapSmall.activeSelf;

        minimapSmall.SetActive(!isSmallActive);
        minimapFull.SetActive(isSmallActive);
    }
}