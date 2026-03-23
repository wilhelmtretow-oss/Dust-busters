using UnityEngine;
using UnityEngine.SceneManagement;
public class PressAnyKey : MonoBehaviour
{
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

}
