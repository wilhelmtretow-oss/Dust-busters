using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioClip clip;

    public void PlaySound()
    {
        UIAudioPlayer.Instance.Play(clip);
    }
}