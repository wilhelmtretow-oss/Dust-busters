using UnityEngine;
using TMPro; // Required for TextMeshPro

public class HelpAdvance : MonoBehaviour
{
    public TMP_Text helpText;       // Drag your TextMeshPro UI here
    public string[] dialogueLines;  // Put your dialogue lines here
    private int currentLine = 0;

    void Start()
    {
        if (dialogueLines.Length > 0)
        {
            helpText.text = dialogueLines[0]; // Show first line
        }
    }

    // Call this from a Button's OnClick
    public void NextLine()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            helpText.text = dialogueLines[currentLine]; // Replace text
        }
        else
        {
            helpText.text = dialogueLines[0];
            currentLine = 0;
        }
    }
}