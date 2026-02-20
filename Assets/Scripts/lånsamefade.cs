using UnityEngine;

public class CleanableObject : MonoBehaviour
{
    public string playerTag = "Player";
    public float fadeDuration = 2f;

    private SpriteRenderer sr;
    private bool playerOnObject = false;
    private float fadeTimer = 0f;
    private Color originalColor;
    private bool isDestroyed = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    void Update()
    {
        if (playerOnObject && !isDestroyed)
        {
            fadeTimer += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            if (fadeTimer >= fadeDuration)
            {
                isDestroyed = true;

                // Uppdatera progress FÖRST
                FindObjectOfType<CleaningManager>().AddCleanedObject();

                // Förstör objektet
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            playerOnObject = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            playerOnObject = false;
        }
    }
}