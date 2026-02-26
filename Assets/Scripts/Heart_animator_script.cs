using UnityEngine;
using UnityEngine.UI;

public class HeartsAnimator : MonoBehaviour
{
    public Image[] hearts;
    public Health healthScript;
    private int nrOfHearts;

    void Start()
    {
        nrOfHearts = hearts.Length;
    }

    void Update()
    {
        float frame = (float)healthScript.CurrentHealth / (float)healthScript.maxHealth;
        DisplayHearts(Mathf.RoundToInt(frame * nrOfHearts));
    }

    void DisplayHearts(int count)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < count ? true : false;
        }
    }
}
