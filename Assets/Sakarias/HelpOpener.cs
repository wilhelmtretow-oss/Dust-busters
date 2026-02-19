using UnityEngine;

public class HelpOpener : MonoBehaviour
{
    public GameObject dialoguePanel; 

    // Open the panel
    public void OpenDialogue()
    {
        dialoguePanel.SetActive(true);
    }

    // Close the panel
    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
