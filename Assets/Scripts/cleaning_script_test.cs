using UnityEngine;

public class DirtCleanable : MonoBehaviour
{
    public string playerTag = "Player";
    public SpriteRenderer spriteRenderer;

    [Header("Cleaning Settings")]
    public int radius = 4;             // hur stor yta som rensas per punkt
    public int samples = 7;            // punkter över bilens bredd
    public float cleanThreshold = 0.1f; // när objektet räknas som städat

    private Texture2D dirtTexture;
    private bool isDestroyed = false;
    private CleaningManager cleaningManager;
    private int width, height;

    void Start()
    {
        cleaningManager = FindObjectOfType<CleaningManager>();

        dirtTexture = Instantiate(spriteRenderer.sprite.texture);
        width = dirtTexture.width;
        height = dirtTexture.height;

        spriteRenderer.sprite = Sprite.Create(
            dirtTexture,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f),
            spriteRenderer.sprite.pixelsPerUnit
        );
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && !isDestroyed)
        {
            BoxCollider2D box = collision.GetComponent<BoxCollider2D>();
            if (box != null)
            {
                Vector3 bottomLeft = box.bounds.min;
                Vector3 bottomRight = new Vector3(box.bounds.max.x, box.bounds.min.y, box.bounds.min.z);

                for (int i = 0; i < samples; i++)
                {
                    float t = (float)i / (samples - 1);
                    Vector3 worldPoint = Vector3.Lerp(bottomLeft, bottomRight, t);

                    // Liten offset bakåt för borstar
                    worldPoint -= collision.transform.up * 0.05f;

                    // Rensa en mjuk cirkel runt punkten
                    CleanSmooth(worldPoint);
                }
            }
        }
    }

    void CleanSmooth(Vector2 worldPos)
    {
        Vector2 localPos = transform.InverseTransformPoint(worldPos);
        int centerX = Mathf.RoundToInt((localPos.x + 0.5f) * width);
        int centerY = Mathf.RoundToInt((localPos.y + 0.5f) * height);

        int rSquared = radius * radius;

        int xStart = Mathf.Max(centerX - radius, 0);
        int xEnd = Mathf.Min(centerX + radius, width - 1);
        int yStart = Mathf.Max(centerY - radius, 0);
        int yEnd = Mathf.Min(centerY + radius, height - 1);

        for (int x = xStart; x <= xEnd; x++)
        {
            for (int y = yStart; y <= yEnd; y++)
            {
                int dx = x - centerX;
                int dy = y - centerY;
                if (dx * dx + dy * dy <= rSquared)
                {
                    Color c = dirtTexture.GetPixel(x, y);
                    if (c.a > 0)
                        dirtTexture.SetPixel(x, y, Color.clear);
                }
            }
        }

        dirtTexture.Apply();
        CheckIfCleaned();
    }

    void CheckIfCleaned()
    {
        if (isDestroyed) return;

        Color[] pixels = dirtTexture.GetPixels();
        int dirtyPixels = 0;
        foreach (Color c in pixels)
            if (c.a > 0.1f)
                dirtyPixels++;

        float dirtyPercent = (float)dirtyPixels / pixels.Length;

        if (dirtyPercent < cleanThreshold)
        {
            isDestroyed = true;
            cleaningManager?.AddCleanedObject();
            Destroy(gameObject);
        }
    }
}