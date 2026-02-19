using UnityEngine;

public class CleanableObject : MonoBehaviour
{
    public string playerTag = "Player";
    public float fadeDuration = 2f; // Tid för fade

    private SpriteRenderer sr;
    private bool isFading = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && !isFading)
        {
            isFading = true;
            // Starta fade
            StartCoroutine(FadeAndDestroy());
        }
    }

    private System.Collections.IEnumerator FadeAndDestroy()
    {
        float timer = 0f;
        Color originalColor = sr.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // När fade är klart, uppdatera progress och förstör objekt
        FindObjectOfType<CleaningManager>().AddCleanedObject();
        Destroy(gameObject);
    }
}
