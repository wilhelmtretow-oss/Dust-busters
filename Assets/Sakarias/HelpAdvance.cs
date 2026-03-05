using UnityEngine;
using TMPro; // Required for TextMeshPro

public class HelpAdvance : MonoBehaviour
{
    public TMP_Text helpText;       // Drag your TextMeshPro UI here
    public string[] dialogueLines;  // Put your dialogue lines here

    public TMP_Text currentText;
    private int currentLine = 0;

    void Start()
    {
        if (dialogueLines.Length > 0)
        {
            UpdateUI();
        }
    }

    // Call this from a Button's OnClick
    public void NextLine()
    {
        currentLine = (currentLine + 1) % dialogueLines.Length;

        UpdateUI();
    }
    public void UpdateUI()
    {
        helpText.text = dialogueLines[currentLine];

        currentText.text = (currentLine + 1) + " / " + dialogueLines.Length;
    }
}