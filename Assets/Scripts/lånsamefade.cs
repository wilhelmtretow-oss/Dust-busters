using System.Collections;
using UnityEngine;

public class FadeOnPlayerTrigger : MonoBehaviour
{
    public string collidingTag = "Player";
    public float fadeDuration = 10f;

    private SpriteRenderer sr;
    private float fadeTimer = 0f;
    private bool isPlayerOnTop = false;
    private Color originalColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void Update()
    {
        if (isPlayerOnTop)
        {
            fadeTimer += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            if (fadeTimer >= fadeDuration)
            {
                Destroy(gameObject);
                FindObjectOfType<CleaningManager>().AddCleanProgress();

            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(collidingTag))
        {
            isPlayerOnTop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(collidingTag))
        {
            isPlayerOnTop = false;
        }
    }
}
